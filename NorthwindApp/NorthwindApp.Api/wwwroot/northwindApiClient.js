$(document).ready(function () {
    $("#categoriesBtn").on("click", function () {
        var uri = "api/categories";

        getList(uri,
            function () {
                return $('<div class="col-xs-3">').text("Category name").prop("outerHTML") +
                    $('<div class="col-xs-6">').text("Description").prop("outerHTML");
            },
            function (item) {
                return $('<div class="col-xs-3">').text(item.categoryName).prop("outerHTML") +
                    $('<div class="col-xs-6">').text(item.description).prop("outerHTML");
            });
    });

    $("#productsBtn").on("click", function () {
        var uri = "api/products?";
        var params = {
            page: 1,
            pageSize: 5
        };

        getList(uri + $.param(params),
            function () {
                return $('<div class="col-xs-3">').text("Product name").prop("outerHTML") +
                    $('<div class="col-xs-3">').text("Quantity per unit").prop("outerHTML") +
                    $('<div class="col-xs-3">').text("Category").prop("outerHTML") +
                    $('<div class="col-xs-3">').text("Supplier company").prop("outerHTML");
            },
            function (item) {
                return $('<div class="col-xs-3">').text(item.productName).prop("outerHTML") +
                    $('<div class="col-xs-3">').text(item.quantityPerUnit).prop("outerHTML") +
                    $('<div class="col-xs-3">').text(item.category.categoryName).prop("outerHTML") +
                    $('<div class="col-xs-3">').text(item.supplier.companyName).prop("outerHTML");
            });
    });

    function getList(uri, headerCallback, itemCallback) {
        $.getJSON(uri)
            .done(function (data) {
                var $results = $("#results");
                $results.html("");

                if (data && data.length > 0) {
                    $('<div class="row item-header">').html(headerCallback()).appendTo($results);

                    $.each(data, function (key, item) {
                        $('<div class="row">').html(itemCallback(item)).appendTo($results);
                    });
                }
            });
    }
});