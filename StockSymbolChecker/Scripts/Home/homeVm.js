const stockChartElementName = "stockChart";

function HomeViewModel() {
    const self = this;

    // Form Data
    self.stockSymbol = ko.observable("").extend({ required: { message: 'Required' } });
    self.stockDate = ko.observable("");
    self.dateFrom = ko.observable("").extend({
        required: {
            onlyIf: () => self.stockDate() === "Custom",
            message: "Required"
        },
        validation: {
            validator: (val) => {
                const dateTo = self.dateTo();
                return !val || !dateTo || new Date(val) <= new Date(dateTo);
            },
            message: "Date From cannot be after Date To.",
            onlyIf: () => self.stockDate() === "Custom"
        }
    });
    self.dateTo = ko.observable("").extend({
        required: {
            onlyIf: () => self.stockDate() === "Custom",
            message: "Required"
        },
        validation: {
            validator: (val) => {
                const dateFrom = self.dateFrom();
                return !val || !dateFrom || new Date(val) >= new Date(dateFrom);
            },
            message: "Date To cannot be before Date From.",
            onlyIf: () => self.stockDate() === "Custom"
        }
    });

    // API Response
    self.stockDetails = ko.observable(null);
    self.eodData = ko.observableArray([]);

    // Page State
    self.isLoading = ko.observable(false);
    self.noDataFound = ko.observable(false);

    // Computed Observables
    self.isCustomDateSelected = ko.computed(() => self.stockDate() === "Custom");

    // Chart Instance
    self.stockChartInstance = null;

    // Search Function
    self.search = () => {
        if (self.errors().length === 0) {
            self.isLoading(true);
            self.noDataFound(false);

            const requestData = {
                stockSymbol: self.stockSymbol(),
                stockDate: self.stockDate(),
                dateFrom: self.dateFrom(),
                dateTo: self.dateTo()
            };

            $.ajax({
                url: '/Home/SearchStock',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(requestData),
                success: (response) => {
                    const data = response.stockApiRoot.data;
                    if (data.length !== 0) {
                        self.stockDetails(data);
                        self.eodData(data.eod);
                        updateChart(data.eod);
                    } else {
                        self.noDataFound(true);
                        destroyChart();
                    }
                    self.isLoading(false);
                },
                error: (error) => {
                    console.error("Error: ", error);
                    self.isLoading(false);
                }
            });
        } else {
            self.errors.showAllMessages();
        }
    };

    // Update Chart Function
    const updateChart = (data) => {
        const labels = data.map(item => item.date);
        const closePrices = data.map(item => item.close);

        const stockChartElement = document.getElementById(stockChartElementName).getContext('2d');

        destroyChart();

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
    };

    // Destroy Chart Function
    const destroyChart = () => {
        if (self.stockChartInstance) {
            self.stockChartInstance.destroy();
        }
    };

    // Apply Validations
    self.errors = ko.validation.group(self);
}

// Apply Knockout Bindings
ko.applyBindings({ homeviewModel: new HomeViewModel() });
