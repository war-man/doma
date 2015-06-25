var app = angular.module("main_app", []);

app.service('my_function_services', function ($http) {

    var pasreimg = function (text) {
        var count = 0;
        var imgs = [];
        var item = {};
        item.link = "";
        for (var i = 0; i < text.length; i++) {
            if (text[i] != ' ') {
                item.link = item.link + text[i];
            }
            else {
                item.number = count;
                imgs.push(item);
                item = {};
                item.link = "";
                count += 1;
            }
        }
        item.number = count;
        imgs.push(item);
        return imgs;
    }
    var getproductinfo = function (id) {
        var item = {};
        $http.get("../../home/getproductinfor?id=" + id).success(function (data) {
            if (data) {
                item.name = data.Ten;
                item.price = data.DonGia;
                item.detail = data.MoTa;
                item.number = 1;
                item.imgs = pasreimg(data.linkanh);
                item.description = data.MoTa;
                item.id = data.ID;
                item.firstimg = pasreimg(data.linkanh)[0].link;
            }
        })
           .error(function () {
           })

        return item;
    }

    return {
        pasreimg: pasreimg,
        getproductinfo: getproductinfo
    }
})

app.service('cart_service', function ($window) {

    var cart = [];

    var add_product_to_cart = function (product, idbosanpham) {
        if (idbosanpham)
        {
            alert("co id bo san pham");
            product.idbosanpham = idbosanpham;
        }
        else {
            alert("KHONG co id bo san pham");
            product.idbosanpham = -1;
        }
        for (var i = 0; i < cart.length; i++) {
            if (cart[i].id == product.id) {
                cart[i] = product;
                save_cart();
                return;
            }
        }
        cart.push(product);
        save_cart();
    }

    var save_cart = function () {
        localStorage.setItem("cart", JSON.stringify(cart));
    }

    var loadcart = function () {
        cart = JSON.parse(localStorage.getItem("cart"));
        remove_double_item();
    }

    var init = function () {
        loadcart();
    }

    var clean = function () {
        cart = [];
        $window.localStorage.clear();
    }

    var remove_double_item = function () {
        var testid = [];
        if (cart) {
            for (var i = 0; i < cart.length; i++) {
                for (var j = 0; j < testid.length; j++) {
                    if (testid[j] == cart[i].id) {
                        cart.splice(i, 1);
                        break;
                    }
                }
                if (j == testid.length) {
                    testid.push(cart[i].id)
                }
            }
        }
        else {
            cart = [];
        }

    }

    var getcart = function () {
        return cart;
    }

    var removeproduct = function (id) {
        if (cart)
        {
            for (var i = 0; i < cart.length; i++) {
                if (cart[i].id == id) {
                    cart.splice(i, 1);
                }
            }
        }
        save_cart();
    }

    var getitemincart = function (id) {
        for (var i = 0; i < cart.length; i++) {
            if(cart[i].id == id)
            {
                return cart[i];
            }
        }
        return null;
    }
    return {
        add_product_to_cart: add_product_to_cart,
        init: init,
        loadcart: loadcart,
        getcart: getcart,
        clean: clean,
        remove_double_item: remove_double_item,
        removeproduct: removeproduct,
        getitemincart: getitemincart
    }
})

app.directive('ncgRequestVerificationToken', ['$http', function ($http) {
    return function (scope, element, attrs) {
        $http.defaults.headers.common['RequestVerificationToken'] = attrs.ncgRequestVerificationToken || "no request verification token";
    };
}]);