using CreateInvoiceSystem.E2E.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CreateInvoiceSystem.E2E.Steps
{
    [Binding]
    public class DashboardSteps
    {
        private readonly DashboardPage _dashboardPage;

        public DashboardSteps(DashboardPage dashboardPage)
        {
            _dashboardPage = dashboardPage;
        }

        [Then(@"I should see the dashboard header")]
        public async Task ThenIShouldSeeTheDashboardHeader()
        {
            var text = await _dashboardPage.WelcomeHeader.InnerTextAsync();
            text.Should().Contain("Witaj w systemie");        
        }

        [Then(@"I should see the statistics section")]
        public async Task ThenIShouldSeeTheStatisticsSection()
        {
            var visible = await _dashboardPage.StatsSection.IsVisibleAsync();
            visible.Should().BeTrue("the statistics section should be visible");
        }

        [Then(@"I should see the quick actions")]
        public async Task ThenIShouldSeeTheQuickActions()
        {
            var visible = await _dashboardPage.QuickActions.IsVisibleAsync();
            visible.Should().BeTrue("the quick actions section should be visible");
        }

        [Then(@"I should see the recent invoices section")]
        public async Task ThenIShouldSeeTheRecentInvoicesSection()
        {
            var visible = await _dashboardPage.RecentInvoices.IsVisibleAsync();
            visible.Should().BeTrue("the recent invoices section should be visible");
        }

        [Then(@"I should see the latest clients section")]
        public async Task ThenIShouldSeeTheLatestClientsSection()
        {
            var visible = await _dashboardPage.LatestClients.IsVisibleAsync();
            visible.Should().BeTrue("the latest clients section should be visible");
        }
    }
}
