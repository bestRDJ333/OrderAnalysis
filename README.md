## 說明
html-Verson資料夾是「HTML的前端畫面」

[完成部份]
* masterPage.html    主版頁面、購物車
* productClass.html  餐點種類
* product.html       餐品分類、餐品明細
* checkOut.html      結帳
* index.html	     首頁
* OrderDetail	     訂單明細

[bug已修正]
* 網站頁面的超連結
* checkOut.cshtml：按下[送出訂單],alert不閃退。

[Bug部份]

* index.html 首頁
	
	* Hero-area 位置跑掉了，疑似在MVC架構下抓不到Navbar的大小？
	  * 字體疑似被覆蓋掉
	* scollTop在chrom 的RWD 畫面停住會消失，畫面滾動會閃爍出現。但在 fireFox 正常顯示
	
* checkOut.cshtml 結帳
	- 按下[送出訂單]後，alert會閃退
	
	

- DDDDDDDDDDebug
	- 會員判定
	- 會員ID傳遞實裝
	- CheckOut頁面修改數量後ShopCart同步更新
	- 結帳點選確認才送出post


[未完成]
* 後台-圖表
* 後台-顧客滿意度


