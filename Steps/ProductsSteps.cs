using CreateInvoiceSystem.E2E.Pages;
using FluentAssertions;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace CreateInvoiceSystem.E2E.Steps
{
    [Binding]
    public class ProductsSteps
    {
        private readonly ProductsPage _productsPage;

        public ProductsSteps(ProductsPage productsPage)
        {
            _productsPage = productsPage;
        }

        [Then(@"I should see the products header")]
        public async Task ThenIShouldSeeTheProductsHeader()
        {
            (await _productsPage.Header.IsVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should see the search input")]
        public async Task ThenIShouldSeeTheSearchInput()
        {
            (await _productsPage.SearchInput.IsVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should see the export button")]
        public async Task ThenIShouldSeeTheExportButton()
        {
            (await _productsPage.ExportCsvButton.IsVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should see the add product button")]
        public async Task ThenIShouldSeeTheAddProductButton()
        {
            (await _productsPage.AddProductButton.IsVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should see the products table")]
        public async Task ThenIShouldSeeTheProductsTable()
        {
            (await _productsPage.ProductsTable.IsVisibleAsync()).Should().BeTrue();
        }

        [Then(@"the products table should have at least 1 row")]
        public async Task ThenTheProductsTableShouldHaveAtLeastOneRow()
        {
            var count = await _productsPage.ProductRows.CountAsync();
            count.Should().BeGreaterThan(0);
        }

        [Then(@"each product row should have Edit and Delete buttons")]
        public async Task ThenEachProductRowShouldHaveEditAndDeleteButtons()
        {
            (await _productsPage.EditButtons.CountAsync()).Should().BeGreaterThan(0);
            (await _productsPage.DeleteButtons.CountAsync()).Should().BeGreaterThan(0);
        }

        [When(@"I search for '(.*)'")]
        public async Task WhenISearchFor(string text)
        {
            await _productsPage.Search(text);
        }

        [Then(@"all visible products should contain '(.*)'")]
        public async Task ThenAllVisibleProductsShouldContain(string text)
        {
            var rows = await _productsPage.ProductRows.AllAsync();

            foreach (var row in rows)
            {
                var name = await row.Locator("td strong").InnerTextAsync();
                name.Should().Contain(text);
            }
        }        

        [When(@"I click the add product button")]
        public async Task WhenIClickTheAddProductButton()
        {
            await _productsPage.ClickAddProduct();
        }

        [Then(@"the add product modal should be visible")]
        public async Task ThenTheAddProductModalShouldBeVisible()
        {
            (await _productsPage.AddProductModal.IsVisibleAsync()).Should().BeTrue();
        }

        [When(@"I submit the add product form")]
        public async Task WhenISubmitTheAddProductForm()
        {
            await _productsPage.SubmitAddProductForm();
        }

        [When(@"I fill the name with '(.*)'")]
        public async Task WhenIFillTheNameWith(string name)
        {
            await _productsPage.ModalNameInput.FillAsync(name);
        }

        [When(@"I fill the price with '(.*)'")]
        public async Task WhenIFillThePriceWith(string price)
        {
            await _productsPage.ModalPriceInput.FillAsync(price);
        }

        [Then(@"the name validation message should be visible")]
        public async Task ThenTheNameValidationMessageShouldBeVisible()
        {
            (await _productsPage.NameValidation.IsVisibleAsync()).Should().BeTrue();
        }

        [Then(@"the price validation message should be visible")]
        public async Task ThenThePriceValidationMessageShouldBeVisible()
        {
            (await _productsPage.PriceValidation.IsVisibleAsync()).Should().BeTrue();
        }               

        [When(@"I click edit on the first product")]
        public async Task WhenIClickEditOnTheFirstProduct()
        {
            await _productsPage.ClickEditFirstProduct();
        }

        [Then(@"the url should contain '(.*)'")]
        public async Task ThenTheUrlShouldContain(string fragment)
        {
            await Assertions.Expect(_productsPage.Page)
                .ToHaveURLAsync(new Regex(Regex.Escape(fragment)));
        }

        [When(@"I click delete on the first product")]
        public async Task WhenIClickDeleteOnTheFirstProduct()
        {
            await _productsPage.ClickDeleteFirstProduct();
        }

        [Then(@"a delete confirmation modal should appear")]
        public async Task ThenADeleteConfirmationModalShouldAppear()
        {
            (await _productsPage.DeleteModal.IsVisibleAsync()).Should().BeTrue();
        }

        [Then(@"I should see pagination controls")]
        public async Task ThenIShouldSeePaginationControls()
        {
            (await _productsPage.Pagination.IsVisibleAsync()).Should().BeTrue();
        }

        [When(@"I navigate to the products page")]
        public async Task WhenINavigateToTheProductsPage()
        {
            await _productsPage.ProductsNavLink.ClickAsync();

            await Assertions.Expect(_productsPage.Header)
                .ToBeVisibleAsync(new() { Timeout = 5000 });
        }

        [When(@"I fill the description with '(.*)'")]
        public async Task WhenIFillTheDescriptionWith(string desc)
        {
            await _productsPage.FillDescription(desc);
        }

        [Then(@"the add product modal should close")]
        public async Task ThenTheAddProductModalShouldClose()
        {            
            await Assertions.Expect(_productsPage.AddProductModal)
                .Not.ToBeVisibleAsync(new() { Timeout = 5000 });
        }

        [Then(@"the products table should contain '(.*)'")]
        public async Task ThenTheProductsTableShouldContain(string name)
        {
            var exists = await _productsPage.TableContainsProduct(name);
            exists.Should().BeTrue($"Expected product '{name}' to appear in the table.");
        }
        private string _dynamicProductName = string.Empty;

        [When(@"I fill the name with dynamic product name")]
        public async Task WhenIFillTheNameWithDynamicProductName()
        {
            _dynamicProductName = $"Test Produkt E2E {DateTime.Now:yyyyMMdd_HHmmss}";
            await _productsPage.ModalNameInput.FillAsync(_dynamicProductName);
        }

        [Then(@"the products table should contain dynamic product name")]
        public async Task ThenTheProductsTableShouldContainDynamicProductName()
        {
            await Assertions.Expect(
                _productsPage.Page.Locator("table.table tbody")
            ).ToContainTextAsync(_dynamicProductName, new() { Timeout = 5000 });
        }
        private int _productCountBefore;

        [When(@"I count the products in the table")]
        public async Task WhenICountTheProductsInTheTable()
        {
            _productCountBefore = await _productsPage.GetProductCount();
        }        

        [Then(@"the products count should be decreased by 1")]
        public async Task ThenTheProductsCountShouldBeDecreasedBy1()
        {
            var countAfter = await _productsPage.GetProductCount();
            countAfter.Should().Be(_productCountBefore - 1);
        }

        [When(@"I delete the product with dynamic product name")]
        public async Task WhenIDeleteTheProductWithDynamicProductName()
        {
            await _productsPage.DeleteProductByName(_dynamicProductName);
        }

        [Then(@"the products table should not contain dynamic product name")]
        public async Task ThenTheProductsTableShouldNotContainDynamicProductName()
        {
            await Assertions.Expect(
                _productsPage.Page.Locator("table.table tbody")
            ).Not.ToContainTextAsync(_dynamicProductName, new() { Timeout = 5000 });
        }

        [When(@"I search for dynamic product name")]
        public async Task WhenISearchForDynamicProductName()
        {
            await _productsPage.Search(_dynamicProductName);
        }

        [Then(@"all visible products should contain dynamic product name")]
        public async Task ThenAllVisibleProductsShouldContainDynamicProductName()
        {
            await Assertions.Expect(
                _productsPage.Page.Locator("table.table tbody")
            ).ToContainTextAsync(_dynamicProductName, new() { Timeout = 5000 });
        }
    }
}
