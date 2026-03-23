using Business.Core.Models.Analytics;
using Business.Core.Models.Incomes;
using Business.Core.Models.Repositories;

namespace Business.Core.Models.Handlers;

/// <summary>
///  Хендлер для аналитики по доходам за месяц
/// </summary>
public class MonthlyReportHandler
{
    private readonly IncomeJsonRepository _incomeRepository;
        private const decimal IndividualTaxRate = 0.04m;
        private const decimal LegalEntityTaxRate = 0.06m;

        public MonthlyReportHandler(IncomeJsonRepository incomeRepository)
        {
            _incomeRepository = incomeRepository ?? throw new ArgumentNullException(nameof(incomeRepository));
        }

        public MonthlyReport Read(int year, int month)
        {
            var entries = _incomeRepository.ReadAll()
                .Where(e => e.Date.Year == year && e.Date.Month == month)
                .ToList();

            return BuildReport(year, month, entries);
        }

        public IEnumerable<MonthlyReport> ReadAll()
        {
            var grouped = _incomeRepository.ReadAll()
                .GroupBy(e => (e.Date.Year, e.Date.Month))
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month);

            foreach (var group in grouped)
            {
                yield return Read(group.Key.Year, group.Key.Month);
            }
        }

        public decimal GetTotalIncome()
        {
            return _incomeRepository.ReadAll().Sum(e => e.Amount);
        }

        public MonthlyReport GetReportForPeriod(DateTime start, DateTime end)
        {
            var entries = _incomeRepository.ReadByDateRange(start, end).ToList();
            return BuildReport(start.Year, start.Month, entries);
        }

        private static MonthlyReport BuildReport(int year, int month, List<Income> entries)
        {
            if (!entries.Any())
            {
                throw new KeyNotFoundException($"There are no entries for this month and year: {year}.{month}");
            }

            decimal incomeTotal = entries.Sum(e => e.Amount);
            decimal incomeIndividuals = entries.Where(e => e.Payer == PayerType.Individual).Sum(e => e.Amount);
            decimal incomeLegalEntities = entries.Where(e => e.Payer == PayerType.LegalEntity).Sum(e => e.Amount);

            decimal taxIndividuals = incomeIndividuals * IndividualTaxRate;
            decimal taxLegalEntities = incomeLegalEntities * LegalEntityTaxRate;
            decimal taxTotal = taxIndividuals + taxLegalEntities;
            decimal profit = incomeTotal - taxTotal;

            return new MonthlyReport(year, month, incomeTotal, incomeIndividuals, incomeLegalEntities,
                                     taxIndividuals, taxLegalEntities, taxTotal, profit);
        }
}