ko.bindingHandlers.dateText = {
    update: function (element, valueAccessor, allBindings) {
        var value = ko.unwrap(valueAccessor());
        var allBindings = allBindings();
        var format = allBindings.dateFormat || 'YYYY-MM-DD';

        // Use moment.js to format the date if it is available
        var formattedDate = value ? moment(value).format(format) : '';

        // Update the text content of the element
        ko.bindingHandlers.text.update(element, function () { return formattedDate; });
    }
};