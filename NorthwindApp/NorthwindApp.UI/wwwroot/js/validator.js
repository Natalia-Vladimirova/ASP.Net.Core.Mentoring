$.validator.addMethod("greaterThanOrEqual", function (value, element, params) {
    var minValue = params[0];

    return value && value >= minValue;
});

$.validator.unobtrusive.adapters.add("greaterThanOrEqual",
    ["minValue"],
    function (options) {
        options.rules["greaterThanOrEqual"] = [parseInt(options.params["minValue"])];
        options.messages["greaterThanOrEqual"] = options.message;
    });
