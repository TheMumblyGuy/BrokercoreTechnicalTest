stockChartElementName = "stockChart";

function HomeViewModel() {
    var self = this;
    self.stockSymbol = ko.observable("");
    self.stockDate = ko.observable("");
    self.data = ko.observableArray([]);
    self.customData = ko.observable("");
    self.isLoading = ko.observable(false); 
    self.allBindingsLoaded = ko.observable(false);

    self.isCustomDateSelected = ko.computed(function () {
        return self.stockDate() === "Custom";
    });

    self.stockChartInstance = null; 

    self.search = function () {
        self.isLoading(true);

        var requestData = {
            stockSymbol: self.stockSymbol(),
            stockDate: self.stockDate()
        };

        $.ajax({
            url: '/Home/SearchStock',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(requestData),
            success: function (response) {
                console.log(response);
                self.data(response.stockApiRoot.data);
                updateChart(response.stockApiRoot.data);
                self.isLoading(false);
            },
            error: function (error) {
                console.log("Error: ", error);
                self.isLoading(false);
            }
        });
    };

    function updateChart(data) {
        var labels = data.map(item => item.date);
        var closePrices = data.map(item => item.close);

        var stockChartElement = document.getElementById(stockChartElementName).getContext('2d');

        // Destroy the previous chart instance if it exists
        if (self.stockChartInstance) {
            self.stockChartInstance.destroy();
        }

        self.stockChartInstance = new Chart(stockChartElement, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Close Price By Day',
                    data: closePrices,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1,
                    fill: false
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

    self.allBindingsLoaded(true);

}

ko.applyBindings({ homeviewModel: new HomeViewModel() });