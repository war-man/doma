app.controller('home_index_controller', ['$scope', '$scope', '$rootScope',
    function ($scope, $scope, $rootScope) {
        $scope.products = [];

        $scope.init = function()
        {
            var item = {};
            item.img = "../../content/my_img/anh1.jpg";
            item.name = "sản phẩm 1";
            item.price = "49.000";
            item.id = 1;
            $scope.products.push(item);

            var item2 = {};
            item2.img = "../../content/my_img/anh1.jpg";
            item2.name = "sản phẩm 2";
            item2.price = "109.000";
            item2.id = 2;
            $scope.products.push(item2);

            var item3 = {};
            item3.img = "../../content/my_img/anh1.jpg";
            item3.name = "sản phẩm 3";
            item3.price = "79.000";
            item3.id = 3;
            $scope.products.push(item3);

            var item4 = {};
            item4.img = "../../content/my_img/anh1.jpg";
            item4.name = "sản phẩm 4";
            item4.price = "39.000";
            item4.id = 4;
            $scope.products.push(item4);

        }

        $scope.addproduct = function (item) {

        }

        $scope.init();
    }]);
