app.controller('home_bosanpham_controller', ['$scope', '$scope', '$rootScope', '$http', 'my_function_services', 'cart_service',
    function ($scope, $scope, $rootScope, $http, my_function_services, cart_service) {

        $scope.slide = [];
        $scope.id = "";
        $scope.products = [];
        $scope.count = 0;
        $scope.init = function(sanpham)
        {
            $scope.id = sanpham.ID;

            for (var i = 0 ; i < sanpham.products.length; i++) {
                var item = {};
                item.ID = sanpham.products[i].ID;
                item.Ten = sanpham.products[i].Ten;
                item.DonGia = sanpham.products[i].DonGia;
                item.soluong = sanpham.products[i].GiaThuongMua;
                item.imgs = sanpham.products[i].linkanh;
              
                var img = {};
                img.number = $scope.count;
                $scope.count += 1;
                img.link = my_function_services.pasreimg(sanpham.products[i].linkanh)[0].link;
                img.caption = item.Ten;
                $scope.slide.push(img);

                $scope.products.push(item);
            }                  
        }

        $scope.themvaogiohang = function () {
            
            for (var i = 0; i < $scope.products.length; i++) {
                cart_service.add_product_to_cart($scope.products[i], $scope.id);
            }
            
            $rootScope.$broadcast('cart_index_controller::recount_amount');
            alert("Đã thêm vào giỏ hàng");
            window.location.href = "../../home/index";
        }
    }]);
