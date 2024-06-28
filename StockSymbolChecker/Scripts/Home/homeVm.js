function HomeViewModel() {
    var self = this;
    self.stockSymbol = ko.observable("");
    self.stockData = ko.observable("");
    self.data = ko.observableArray([]);

    self.search = function () {
        var requestData = {
            stockSymbol: self.stockSymbol(),
            stockData: self.stockData()
        };

        $.ajax({
            url: '/Home/SearchStock',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(requestData),
            success: function (response) {
                console.log(response);
                self.data(response.stockApiRoot.data);
            },
            error: function (error) {
                console.log("Error: ", error);
            }
        });
    };
}

ko.applyBindings({ homeviewModel: new HomeViewModel() });