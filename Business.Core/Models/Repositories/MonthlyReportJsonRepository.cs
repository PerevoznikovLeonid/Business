using Business.Core.Analytics;
using Business.Core.Incomes;

namespace Business.Core.Models.Repositories;

/// <summary>
/// Json репозиторий для хранения аналитики по доходам за месяц
/// </summary>
public class MonthlyReportJsonRepository
{
    private readonly IncomeJsonRepository _incomeRepository;
        private const decimal IndividualTaxRate = 0.04m;
        private const decimal LegalEntityTaxRate = 0.06m;

        public MonthlyReportJsonRepository(IncomeJsonRepository incomeRepository)
        {
            _incomeRepository = incomeRepository ?? throw new ArgumentNullException(nameof(incomeRepository));
        }

        public MonthlyReport GetMonthlyReport(int year, int month)
        {
            var entries = _incomeRepository.ReadAll()
                .Where(e => e.Date.Year == year && e.Date.Month == month)
                .ToList();

            return BuildReport(year, month, entries);
        }

        public IEnumerable<MonthlyReport> GetAllMonthlyReports()
        {
            var grouped = _incomeRepository.ReadAll()
                .GroupBy(e => e.YearMonth)
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month);

            foreach (var group in grouped)
            {
                yield return GetMonthlyReport(group.Key.Year, group.Key.Month);
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

        private MonthlyReport BuildReport(int year, int month, List<Income> entries)
        {
            if (!entries.Any())
            {
                return new MonthlyReport(year, month, 0, 0, 0, 0, 0, 0, 0);
            }

            decimal totalIncome = entries.Sum(e => e.Amount);
            decimal incomeFromIndividuals = entries.Where(e => e.Payer == PayerType.Individual).Sum(e => e.Amount);
            decimal incomeFromLegalEntities = entries.Where(e => e.Payer == PayerType.LegalEntity).Sum(e => e.Amount);

            decimal taxFromIndividuals = incomeFromIndividuals * IndividualTaxRate;
            decimal taxFromLegalEntities = incomeFromLegalEntities * LegalEntityTaxRate;
            decimal totalTax = taxFromIndividuals + taxFromLegalEntities;
            decimal profit = totalIncome - totalTax;

            return new MonthlyReport(year, month, totalIncome, incomeFromIndividuals, incomeFromLegalEntities,
                                     taxFromIndividuals, taxFromLegalEntities, totalTax, profit);
        }
}