app.controller('home_bosanpham_controller', ['$scope', '$scope', '$rootScope', '$http', 'my_function_services', 'cart_service',
    function ($scope, $scope, $rootScope, $http, my_function_services, cart_service) {

        $scope.id = "";
        $scope.name = "";
        $scope.price = "";
        $scope.imgs = "";
        $scope.product = {};
        $scope.soluong = 1;

        $scope.init = function (sanpham) {
            $scope.id = sanpham.ID;
            $scope.name = sanpham.Ten;
            $scope.price = sanpham.DonGia;
            $scope.imgs = my_function_services.pasreimg(sanpham.linkanh);

            $scope.product.imgs = $scope.imgs;
            $scope.product.name = $scope.name;
            $scope.product.number = $scope.soluong;
            $scope.product.price = $scope.price;            
        }

        $scope.themvaogiohang = function () {
            $scope.product.number = $scope.soluong;
            cart_service.add_product_to_cart($scope.product);
            $rootScope.$broadcast('cart_index_controller::recount_amount');
            alert("Đã thêm vào giỏ hàng");
            window.location.href = "../../home/index";
        }
    }]);
