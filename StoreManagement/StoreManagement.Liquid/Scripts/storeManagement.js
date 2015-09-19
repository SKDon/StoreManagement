﻿


$(document).ready(function () {
    //var mm = $("[data-view-contact-resume-contact=" + encodedResumeId + "]");
    //mm.fadeOut('slow', function () {
    //    mm.html(data);
    //    mm.fadeIn('slow');
    //});

    callAjaxMethod();
});
function callAjaxMethod() {

    try {
        GetMainLayout();
    }
    catch (err) {
        console.error(err.message);
    }

    try {
        GetMainFooter();
    }
    catch (err) {
        console.error(err.message);
    }

    try {
        if ($('[data-related-contents]').length > 0) {
            GetRelatedContents();
        }
    }
    catch (err) {
        console.error(err.message);
    }

    try {
        if ($('[data-product-labels]').length > 0) {
            GetProductLabels();
        }
    }
    catch (err) {
        console.error(err.message);
    }

    try {
        if ($('[data-related-products-by-brand]').length > 0) {
            GetRelatedProductsPartialByBrand();
        }
    }
    catch (err) {
        console.error(err.message);
    }

    try {
        if ($('[data-related-products-by-category]').length > 0) {
            GetRelatedProductsPartialByCategory();
        }
    }
    catch (err) {
        console.error(err.message);
    }

    try {
        if ($('[data-product-categories]').length > 0) {
            GetProductCategories();
        }
    }
    catch (err) {
        console.error(err.message);
    }

    try {
        if ($('[data-brands]').length > 0) {
            GetBrands();
        }

    }
    catch (err) {
        console.error(err.message);
    }

    try {
        if ($('[data-popular-products]').length > 0) {
            GetPopularProducts();
        }

    }
    catch (err) {
        console.error(err.message);
    }

    try {
        if ($('[data-recent-products]').length > 0) {
            GetRecentProducts();
        }
    }
    catch (err) {
        console.error(err.message);
    }
}
function GetPopularProducts() {

    $('[data-popular-products]').each(function () {
        var truethis = this;

        var page = parseInt($(this).attr('data-page'));
        var designName = $(this).attr('data-template-design-name');
        var categoryId = GetValueInt($(this).attr('data-product-category-id'));
        var brandId = GetValueInt($(this).attr('data-brand-id'));
        var imageWidth = GetValueInt($(this).attr('data-image-width'));
        var imageHeight = GetValueInt($(this).attr('data-image-height'));
        var pageSize = GetValueInt($(this).attr('data-page-size'));

        var postData = JSON.stringify({
            "page": page,
            "designName": designName,
            "categoryId": categoryId,
            "brandId": brandId,
            "imageWidth": imageWidth,
            "imageHeight": imageHeight,
            "pageSize": pageSize
        });

        ajaxMethodCall(postData, "/AjaxProducts/GetPopularProducts", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
            $(truethis).attr('data-page', page + 1);
        });
    });
}
function GetRecentProducts() {

    $('[data-recent-products]').each(function () {
        var truethis = this;

        var page = parseInt($(this).attr('data-page'));
        var designName = $(this).attr('data-template-design-name');
        var categoryId = GetValueInt($(this).attr('data-product-category-id'));
        var brandId = GetValueInt($(this).attr('data-brand-id'));
        var imageWidth = GetValueInt($(this).attr('data-image-width'));
        var imageHeight = GetValueInt($(this).attr('data-image-height'));
        var pageSize = GetValueInt($(this).attr('data-page-size'));

        var postData = JSON.stringify({
            "page": page,
            "designName": designName,
            "categoryId": categoryId,
            "brandId": brandId,
            "imageWidth": imageWidth,
            "imageHeight": imageHeight,
            "pageSize": pageSize
        });

        ajaxMethodCall(postData, "/AjaxProducts/GetRecentProducts", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
            $(truethis).attr('data-page', page + 1);
        });
    });
}
function GetProductLabels() {
    $('[data-product-labels]').each(function () {
        var truethis = this;
        var designName = $(this).attr('data-template-design-name');
        var itemid = $(this).attr('data-product-itemid');
        var itemtype = 'product';
        var postData = JSON.stringify({ id: itemid, "designName": designName });
        ajaxMethodCall(postData, "/AjaxProducts/GetProductLabels", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
        });
    });
}
function GetMainFooter() {
    $('[data-main-footer]').each(function () {
        var truethis = this;
        var postData = JSON.stringify({});
        ajaxMethodCall(postData, "/AjaxGenerics/MainLayoutFooter", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
        });
    });
}
function GetMainLayout() {
    $('[data-main-navigation]').each(function () {
        var truethis = this;
        var postData = JSON.stringify({});
        ajaxMethodCall(postData, "/AjaxGenerics/MainLayout", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
        });
    });
}
function GetBrands() {

    $('[data-brands]').each(function () {
        var truethis = this;

        var designName = $(this).attr('data-template-design-name');
        var imageWidth = GetValueInt($(this).attr('data-image-width'));
        var imageHeight = GetValueInt($(this).attr('data-image-height'));

        var postData = JSON.stringify({
            "designName": designName,
            "imageWidth": imageWidth,
            "imageHeight": imageHeight
        });
        ajaxMethodCall(postData, "/AjaxProducts/GetBrands", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
        });
    });
}
function GetProductCategories() {

    $('[data-product-categories]').each(function () {
        var truethis = this;
        var designName = $(this).attr('data-template-design-name');
        var imageWidth = GetValueInt($(this).attr('data-image-width'));
        var imageHeight = GetValueInt($(this).attr('data-image-height'));
        var postData = JSON.stringify({
            "designName": designName,
            "imageWidth": imageWidth,
            "imageHeight": imageHeight
        });
        ajaxMethodCall(postData, "/AjaxProducts/GetProductCategories", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
        });
    });
}
function GetRelatedContents() {
    $('[data-related-contents]').each(function () {
        var truethis = this;
        var designName = $(this).attr('data-template-design-name');
        var contentType = $(this).attr('data-related-contents-type');
        var categoryId = $(this).attr('data-related-contents');
        var imageWidth = GetValueInt($(this).attr('data-image-width'));
        var imageHeight = GetValueInt($(this).attr('data-image-height'));
        var take = GetValueInt($(this).attr('data-number-of-items'));
        var postData = JSON.stringify({
            "categoryId": categoryId,
            "contentType": contentType,
            "designName": designName,
            "take": take,
            "imageWidth": imageWidth,
            "imageHeight": imageHeight
        });
        ajaxMethodCall(postData, "/AjaxContents/GetRelatedContents", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
        });
    });
}

function GetRelatedProductsPartialByCategory() {
    $('[data-related-products-by-category]').each(function () {
        var truethis = this;
        var designName = $(this).attr('data-template-design-name');
        var productCategoryId = $(this).attr('data-related-products-by-category');
        var excludedProductId = GetValueInt($(this).attr('data-excluded-product-id'));
        var imageWidth = GetValueInt($(this).attr('data-image-width'));
        var imageHeight = GetValueInt($(this).attr('data-image-height'));
        var take = GetValueInt($(this).attr('data-number-of-items'));
        var postData = JSON.stringify({
            "categoryId": productCategoryId,
            "excludedProductId": excludedProductId,
            "designName": designName,
            "take": take,
            "imageWidth": imageWidth,
            "imageHeight": imageHeight
        });
        ajaxMethodCall(postData, "/AjaxProducts/GetRelatedProductsPartialByCategory", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
        });
    });
}
function GetRelatedProductsPartialByBrand() {
    $('[data-related-products-by-brand]').each(function () {
        var truethis = this;
        var designName = $(this).attr('data-template-design-name');
        var brandId = GetValueInt($(this).attr('data-related-products-by-brand'));
        var excludedProductId = GetValueInt($(this).attr('data-excluded-product-id'));
        var imageWidth = GetValueInt($(this).attr('data-image-width'));
        var imageHeight = GetValueInt($(this).attr('data-image-height'));
        var take = GetValueInt($(this).attr('data-number-of-items'));
        var postData = JSON.stringify({
            "brandId": brandId,
            "excludedProductId": excludedProductId,
            "designName": designName,
            "take": take,
            "imageWidth": imageWidth,
            "imageHeight": imageHeight
        });
        ajaxMethodCall(postData, "/AjaxProducts/GetRelatedProductsPartialByBrand", function (data) {
            $(truethis).empty();
            $(truethis).html(data).animate({ 'height': '150px' }, 'slow');
        });
    });
}
function GetValueInt(val) {
    val = val === undefined ? 0 : val;
    return val;
}

function ajaxMethodCall(postData, ajaxUrl, successFunction) {

    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: postData,
        success: successFunction,
        error: function (jqXHR, exception) {
            console.error("parameters :" + postData);
            console.error("ajaxUrl :" + ajaxUrl);
            console.error("responseText :" + jqXHR.responseText);
            if (jqXHR.status === 0) {
                console.error('Not connect.\n Verify Network.');
            } else if (jqXHR.status == 404) {
                console.error('Requested page not found. [404]');
            } else if (jqXHR.status == 500) {
                console.error('Internal Server Error [500].');
            } else if (exception === 'parsererror') {
                console.error('Requested JSON parse failed.');
            } else if (exception === 'timeout') {
                console.error('Time out error.');
            } else if (exception === 'abort') {
                console.error('Ajax request aborted.');
            } else {
                console.error('Uncaught Error.\n' + jqXHR.responseText);
            }
        },
        contentType: "application/json",
        dataType: "json"
    });
}
