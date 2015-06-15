app.controller('home_index_controller', ['$scope', '$scope', '$rootScope', '$http', 'my_function_services',
    function ($scope, $scope, $rootScope, $http, my_function_services) {
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

app.controller('them_gio_hang_modal_controller', ['$scope', '$rootScope', '$http', 'my_function_services','cart_service',
    function ($scope, $rootScope, $http, my_function_services, cart_service) {
        $scope.product = {};

        $scope.themvaogiohang = function () {
            cart_service.add_product_to_cart($scope.product);

            $scope.product = {};
        }

        $scope.huy = function () {
            $scope.product = {};
        }

        $rootScope.$on('them_gio_hang_modal_controller::show', function (event, id) {

            $http.get("../../home/getproductinfor?id=" + id).success(function (data) {
                if (data) {
                    $scope.product.name = data.Ten;
                    $scope.product.price = data.DonGia;
                    $scope.product.detail = data.MoTa;
                    $scope.product.number = 1;
                    $scope.product.imgs = my_function_services.pasreimg(data.linkanh);
                    $scope.product.description = data.MoTa;
                    $scope.product.id = data.ID;                   

                    $("#ThemVaoGioHang_Modal").modal('show');
                }

            })
            .error(function () {
                alert("Lỗi!");
            })


        })
    }]);


app.controller('cart_controller', ['$scope', '$rootScope', '$http', 'my_function_services', 'cart_service',
    function ($scope, $rootScope, $http, my_function_services, cart_service) {
        $scope.products = [];
       
        $scope.init = function () {
            $scope.products = cart_service.getcart();
        }
    }]);

