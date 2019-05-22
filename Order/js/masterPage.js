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