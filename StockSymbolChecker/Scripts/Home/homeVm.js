function HomeViewModel() {
    var self = this;
    self.stockSymbol = ko.observable("");
    self.stockDate = ko.observable("");
    self.data = ko.observableArray([]);
    self.customData = ko.observable(""); 
  

    self.isCustomDateSelected = ko.computed(function () {
        return self.stockDate() === "Custom";
    });


    self.search = function () {
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
                self.data(response.stockApiRoot.data);
                updateChart(response.stockApiRoot.data);
              
            },
            error: function (error) {
                console.log("Error: ", error);
            }
        });
    };

    function updateChart(data) {
        var labels = data.map(item => item.date);
        var closePrices = data.map(item => item.close);

        var ctx = document.getElementById('stockChart').getContext('2d');
        new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Close Price',
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
}

ko.applyBindings({ homeviewModel: new HomeViewModel() });