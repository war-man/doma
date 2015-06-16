app.controller('home_index_controller', ['$scope', '$scope', '$rootScope', '$http', 'my_function_services', 'cart_service',
    function ($scope, $scope, $rootScope, $http, my_function_services, cart_service) {
        $scope.products = [];

        $scope.init = function () {
            $http.get("../../home/getallproducts").success(function (data) {
                for (var i = 0; i < data.length; i++) {
                    $scope.addproduct(data[i]);
                }
            })
            .error(function () {
                alert("Lỗi!");
            })
        }

        $scope.addproduct = function (sanpham) {
            var item = {};
            item.img = my_function_services.pasreimg(sanpham.linkanh)[0].link;
            item.name = sanpham.Ten;
            item.price = sanpham.DonGia;
            item.id = sanpham.ID;
            item.description = sanpham.MoTa;
            $scope.products.push(item);
        }

        $scope.themgiohang = function (id) {
            $rootScope.$broadcast('them_gio_hang_modal_controller::show', id);
        }
    }]);

app.controller('them_gio_hang_modal_controller', ['$scope', '$rootScope', '$http', 'my_function_services', 'cart_service',
    function ($scope, $rootScope, $http, my_function_services, cart_service) {
        $scope.product = {};

        $scope.themvaogiohang = function () {
            cart_service.add_product_to_cart($scope.product);
            $rootScope.$broadcast('cart_index_controller::recount_amount');
            $scope.product = {};
        }

        $scope.huy = function () {
            $scope.product = {};
        }

        $rootScope.$on('them_gio_hang_modal_controller::show', function (event, id, number) {

            $scope.product = cart_service.getitemincart(id);
            if ($scope.product == null)
            {
                $scope.product = my_function_services.getproductinfo(id);
            }           

            if (number)
            {
                $scope.product.number = number;
            }
            if ($scope.product) {
                $("#ThemVaoGioHang_Modal").modal('show');
            }
        })
    }]);

