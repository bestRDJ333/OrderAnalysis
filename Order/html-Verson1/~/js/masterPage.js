//topMenu className的切換----------------
        function toggleClass() {
            var x = document.getElementById("myTopMenu");

            var navlogo = document.getElementById("navLogo");
            var navTitle = document.getElementById("navTitle");


            //切換RWD的css
            if (x.className === "navlinkGroup") {
                x.className += " responsive";
                navTitle.style.display = "block";
            } else {
                x.className = "navlinkGroup";
                navTitle.style.display = "none";
            }
        }

        //navbar點選超連結後，自動收合---------------
        $(function() {
            if ($(window).width() < 769) { //當視窗小於768時才運作
                $(".a-trigger").click(function() {
                    $("#rwdMenu").click();
                });
            }
});

//購物車---------------------------------------
//$(function () {
//    main();
//});

main();

function main() {

    ShowShopCart();
    NoItemCart();
}

//顯示 購物車
function ShowShopCart() {
    //滑出 購物車
    $('#navShopCart').hover(function () {
        $('.shopCart').show('slow');
    });

    $('.shopCart').hover(function () {
        //滑鼠移入事件

    }, function () {
        //滑鼠移出事件--------

        //隱藏購物車
        $('.shopCart').hide();

    });

}

//無商品的購物車--------------------------------
function NoItemCart() {
    var cartCount = document.getElementsByClassName("shopCartItem");

    if (cartCount.length <= 0) {
        //購物車無商品
        $('.noItem').show();
        $('.shopCartFooter').hide();
    }
    else {
        //購物車有商品
        $('.noItem').hide();
        $('.shopCartFooter').show();
    }
}
