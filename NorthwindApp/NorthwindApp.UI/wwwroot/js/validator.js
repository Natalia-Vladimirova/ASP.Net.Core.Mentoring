$.validator.addMethod("greaterThanOrEqual", function (value, element, param) {
    var minValue = parseInt(param);

    return value && parseInt(value) >= minValue;
});
$.validator.unobtrusive.adapters.addSingleVal("greaterThanOrEqual", "minValue");

