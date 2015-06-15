var app = angular.module("main_app", []);

app.service('my_function_services', function () {
   
    var pasreimg = function(text)
    {
        var count = 0;
        var imgs = [];
        var item = {};
        item.link = "";
        for (var i = 0; i < text.length; i++)
        {
            if(text[i] != ' ')
            {
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
    return {
        pasreimg: pasreimg
    }
})

app.service('cart_service', function () {

    var cart = [];

    var add_product_to_cart = function (product) {
        cart.push(product);
        send_to_server();
    }

    var send_to_server = function () {
        localStorage['cart'] = cart;
    }

    var init = function () {
        cart = localStorage['cart'];
    }

    var getcart = function () {
        return cart;
    }
    return {
        add_product_to_cart: add_product_to_cart,
        init: init,
        send_to_server: send_to_server,
        getcart: getcart
    }
})