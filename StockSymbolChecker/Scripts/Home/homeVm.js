stockChartElementName = "stockChart";

function HomeViewModel() {
    var self = this;
    //Form Data
    self.stockSymbol = ko.observable("").extend({ required: true });
    self.stockDate = ko.observable("");
    self.dateFrom = ko.observable("");
    self.dateTo = ko.observable("");

    //API Response
    self.stockDetails = ko.observable(null)
    self.eodData = ko.observableArray([]);

    //Page functions
    self.isLoading = ko.observable(false);
    self.noDataFound = ko.observable(false);

    self.isCustomDateSelected = ko.computed(function () {
        return self.stockDate() === "Custom";
    });

    self.stockChartInstance = null;

    self.search = function () {
        if (self.errors().length == 0) {
            self.isLoading(true);
            self.noDataFound(false);

            var requestData = {
                stockSymbol: self.stockSymbol(),
                stockDate: self.stockDate(),
                dateFrom: self.dateFrom(),
                dateTo: self.dateTo(),
            };

            $.ajax({
                url: '/Home/SearchStock',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(requestData),
                success: function (response) {
                    //console.log(response);

                    if (response.stockApiRoot.data.length !== 0) {
                        self.stockDetails(response.stockApiRoot.data);
                        self.eodData(response.stockApiRoot.data.eod);
                        updateChart(response.stockApiRoot.data.eod);
                    }
                    else {
                        self.noDataFound(true);
                        destoryChart();
                    }
                    self.isLoading(false);
                },
                error: function (error) {
                    console.log("Error: ", error);
                    self.isLoading(false);
                }
            });
        } else {
            self.errors.showAllMessages();
        }
    };

    function updateChart(data) {
        var labels = data.map(item => item.date);
        var closePrices = data.map(item => item.close);

        var stockChartElement = document.getElementById(stockChartElementName).getContext('2d');

        destoryChart();

        self.stockChartInstance = new Chart(stockChartElement, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Close Price By Day',
                    data: closePrices,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1,
                    fill: true
                }]
            },
            options: {
                scales: {
                    x: {
                        type: 'time',
                        time: {
                            unit: 'day'
                        }
                    },
                    y: {
                        beginAtZero: false
                    }
                }
            }
        });
    }

    function destoryChart() {
        // Destroy the previous chart instance if it exists
        if (self.stockChartInstance) {
            self.stockChartInstance.destroy();
        }
    }

    self.errors = ko.validation.group(self);
}

ko.applyBindings({ homeviewModel: new HomeViewModel() });