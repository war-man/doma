var app = angular.module("main_app", []);

app.service('my_function_services', function () {
   
    var pasreimg = function(text)
    {
        var imgs = [];
        var item = "";
        for (var i = 0; i < text.length; i++)
        {
            if(text[i] != ' ')
            {
                item = item + text[i];
            }
            else {
                imgs.push(item);
                item = "";
            }
        }

        imgs.push(item);
        return imgs;
    }
    return {
        pasreimg: pasreimg
    }
})

app.service('gio_hang_service', function () {

    var pasreimg = function (text) {
        var imgs = [];
        var item = "";
        for (var i = 0; i < text.length; i++) {
            if (text[i] != ' ') {
                item = item + text[i];
            }
            else {
                imgs.push(item);
                item = "";
            }
        }
        imgs.push(item);
        return imgs;
    }

    var init = function () {
    }
    return {
        pasreimg: pasreimg,
        init: init
    }
})