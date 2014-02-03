//HOME PAGE PRODUCTS
var Speed_1 = 20; 
var Space_1 = 10; 
var PageWidth_1 = 192 * 1; 
var interval_1 = 2500; 
var fill_1 = 0; 
var MoveLock_1 = false;
var MoveTimeObj_1;
var MoveWay_1="right";
var Comp_1 = 0;
var AutoPlayObj_1=null;
function GetObj(objName){	
	var HomePageProducts_size = $("#List1_1  .item-box").size();
	if (HomePageProducts_size < 5){
		$("#List2_1").css("display","none");
		$("#List2_12").css("display","none");
		$(".home-page-product-grid .homepage_buttons").css("display","none");
		return "";
		}else
		{
	if(document.getElementById)
	{return eval('document.getElementById("'+objName+'")')}
	else
	{return eval('document.all.'+objName)}
	}}
function AutoPlay_1(){
	clearInterval(AutoPlayObj_1);
	AutoPlayObj_1=setInterval('ISL_GoDown_1();ISL_StopDown_1();',interval_1)
	}
function ISL_GoUp_1(){
	if(MoveLock_1)return;clearInterval(AutoPlayObj_1);
	MoveLock_1=true;MoveWay_1="left";
	MoveTimeObj_1=setInterval('ISL_ScrUp_1();',Speed_1);
	}
function ISL_StopUp_1(){
	if(MoveWay_1 == "right"){
		return
		};
		clearInterval(MoveTimeObj_1);
		if((GetObj('ISL_Cont_1').scrollLeft-fill_1)%PageWidth_1!=0){
			Comp_1=fill_1-(GetObj('ISL_Cont_1').scrollLeft%PageWidth_1);
			CompScr_1()
			}
			else{
				MoveLock_1=false
				}
	AutoPlay_1()
	}
function ISL_ScrUp_1(){
	if(GetObj('ISL_Cont_1').scrollLeft<=0){
		GetObj('ISL_Cont_1').scrollLeft=GetObj('ISL_Cont_1').scrollLeft+GetObj('List1_1').offsetWidth
		}
GetObj('ISL_Cont_1').scrollLeft-=Space_1
}
function ISL_GoDown_1(){
	clearInterval(MoveTimeObj_1);
	if(MoveLock_1)return;
	clearInterval(AutoPlayObj_1);
	MoveLock_1=true;
	MoveWay_1="right";
	ISL_ScrDown_1();
	MoveTimeObj_1=setInterval('ISL_ScrDown_1()',Speed_1)
	}
function ISL_StopDown_1(){
	if(MoveWay_1 == "left"){
		return};clearInterval(MoveTimeObj_1);
		if(GetObj('ISL_Cont_1').scrollLeft%PageWidth_1-(fill_1>=0?fill_1:fill_1+1)!=0){
			Comp_1=PageWidth_1-GetObj('ISL_Cont_1').scrollLeft%PageWidth_1+fill_1;CompScr_1()
			}else{
				MoveLock_1=false
				}
AutoPlay_1()}
function ISL_ScrDown_1(){
	if(GetObj('ISL_Cont_1').scrollLeft>=GetObj('List1_1').scrollWidth){
		GetObj('ISL_Cont_1').scrollLeft=GetObj('ISL_Cont_1').scrollLeft-GetObj('List1_1').scrollWidth
		}
GetObj('ISL_Cont_1').scrollLeft+=Space_1
}
function CompScr_1(){
	if(Comp_1==0){
		MoveLock_1=false;
		return
		}
var num,TempSpeed=Speed_1,TempSpace=Space_1;
if(Math.abs(Comp_1)<PageWidth_1/100){
	TempSpace=Math.round(Math.abs(Comp_1/Space_1));
	if(TempSpace<1){
		TempSpace=1
		}}
if(Comp_1<0){
	if(Comp_1<-TempSpace){
		Comp_1+=TempSpace;num=TempSpace
		}else{
			num=-Comp_1;Comp_1=0
			}
GetObj('ISL_Cont_1').scrollLeft-=num;setTimeout('CompScr_1()',TempSpeed)}else{
	if(Comp_1>TempSpace){
		Comp_1-=TempSpace;num=TempSpace
		}else{
			num=Comp_1;Comp_1=0
			}
GetObj('ISL_Cont_1').scrollLeft+=num;setTimeout('CompScr_1()',TempSpeed)}}
function picrun_ini(){
GetObj("List2_1").innerHTML=GetObj("List1_1").innerHTML;
GetObj("List2_12").innerHTML=GetObj("List1_1").innerHTML;
GetObj('ISL_Cont_1').scrollLeft=fill_1>=0?fill_1:GetObj('List1_1').scrollWidth-Math.abs(fill_1);
GetObj("ISL_Cont_1").onmouseover=function(){clearInterval(AutoPlayObj_1)};
GetObj("ISL_Cont_1").onmouseout=function(){AutoPlay_1()};AutoPlay_1();
}



//BESTSELLERS
var Speed_21 = 20; 
var Space_21 = 10; 
var PageWidth_21 = 192 * 1; 
var interval_21 = 2500; 
var fill_21 = 0; 
var MoveLock_21 = false;
var MoveTimeObj_21;
var MoveWay_21="right";
var Comp_21 = 0;
var AutoPlayObj_21=null;	
function GetObj2(objName){
	var bestsellers_size = $("#List1_21  .item-box").size();
	if (bestsellers_size < 5){
		$("#List2_21").css("display","none");
		$("#List2_212").css("display","none");
		$(".bestsellers .homepage_buttons").css("display","none");
		return "";
		}else
		{
			if(document.getElementById)
	{return eval('document.getElementById("'+objName+'")')}
	else
	{return eval('document.all.'+objName)}
	
	}
	}
function AutoPlay_21(){
	clearInterval(AutoPlayObj_21);
	AutoPlayObj_21=setInterval('ISL_GoDown_21();ISL_StopDown_21();',interval_21)
	}
function ISL_GoUp_21(){
	if(MoveLock_21)return;clearInterval(AutoPlayObj_21);
	MoveLock_21=true;MoveWay_21="left";
	MoveTimeObj_21=setInterval('ISL_ScrUp_21();',Speed_21);
	}
function ISL_StopUp_21(){
	if(MoveWay_21 == "right"){
		return
		};
		clearInterval(MoveTimeObj_21);
		if((GetObj2('ISL_Cont_21').scrollLeft-fill_21)%PageWidth_21!=0){
			Comp_21=fill_21-(GetObj2('ISL_Cont_21').scrollLeft%PageWidth_21);
			CompScr_21()
			}
			else{
				MoveLock_21=false
				}
	AutoPlay_21()
	}
function ISL_ScrUp_21(){
	if(GetObj2('ISL_Cont_21').scrollLeft<=0){
		GetObj2('ISL_Cont_21').scrollLeft=GetObj2('ISL_Cont_21').scrollLeft+GetObj2('List1_21').offsetWidth
		}
GetObj2('ISL_Cont_21').scrollLeft-=Space_21
}
function ISL_GoDown_21(){
	clearInterval(MoveTimeObj_21);
	if(MoveLock_21)return;
	clearInterval(AutoPlayObj_21);
	MoveLock_21=true;
	MoveWay_21="right";
	ISL_ScrDown_21();
	MoveTimeObj_21=setInterval('ISL_ScrDown_21()',Speed_21)
	}
function ISL_StopDown_21(){
	if(MoveWay_21 == "left"){
		return
		};clearInterval(MoveTimeObj_21);
		if(GetObj2('ISL_Cont_21').scrollLeft%PageWidth_21-(fill_21>=0?fill_21:fill_21+1)!=0){
			Comp_21=PageWidth_21-GetObj2('ISL_Cont_21').scrollLeft%PageWidth_21+fill_21;CompScr_21()
			}else{
				MoveLock_21=false
				}
AutoPlay_21()}
function ISL_ScrDown_21(){
	if(GetObj2('ISL_Cont_21').scrollLeft>=GetObj2('List1_21').scrollWidth){
		GetObj2('ISL_Cont_21').scrollLeft=GetObj2('ISL_Cont_21').scrollLeft-GetObj2('List1_21').scrollWidth
		}
GetObj2('ISL_Cont_21').scrollLeft+=Space_21
}
function CompScr_21(){
	if(Comp_21==0){
		MoveLock_21=false;
		return
		}
var num2,TempSpeed2=Speed_21,TempSpace2=Space_21;
if(Math.abs(Comp_21)<PageWidth_21/100){
	TempSpace2=Math.round(Math.abs(Comp_21/Space_21));
	if(TempSpace2<1){
		TempSpace2=1
		}}
if(Comp_21<0){
	if(Comp_21<-TempSpace2){
		Comp_21+=TempSpace2;num2=TempSpace2
		}else{
			num2=-Comp_21;Comp_21=0
			}
GetObj2('ISL_Cont_21').scrollLeft-=num2;setTimeout('CompScr_21()',TempSpeed2)}else{
	if(Comp_21>TempSpace2){
		Comp_21-=TempSpace2;num2=TempSpace2
		}else{
			num2=Comp_21;Comp_21=0
			}
GetObj2('ISL_Cont_21').scrollLeft+=num2;setTimeout('CompScr_21()',TempSpeed2)}}
function picrun_ini2(){
GetObj2("List2_21").innerHTML=GetObj2("List1_21").innerHTML;
GetObj2("List2_212").innerHTML=GetObj2("List1_21").innerHTML;
GetObj2('ISL_Cont_21').scrollLeft=fill_21>=0?fill_21:GetObj2('List1_21').scrollWidth-Math.abs(fill_21);
GetObj2("ISL_Cont_21").onmouseover=function(){clearInterval(AutoPlayObj_21)};
GetObj2("ISL_Cont_21").onmouseout=function(){AutoPlay_21()};
AutoPlay_21();
}



//ADD NEWPRODUCTS
var Speed_31 = 20; 
var Space_31 = 10; 
var PageWidth_31 = 192 * 1; 
var interval_31 = 2500; 
var fill_31 = 0; 
var MoveLock_31 = true;
var MoveTimeObj_31;
var MoveWay_31="right";
var Comp_31 = 0;
var AutoPlayObj_31=null;
function GetObj3(objName){
	var RecentlyAddedProducts_size = $("#List1_31  .item-box").size();
	if (RecentlyAddedProducts_size < 5){
		$("#List2_31").css("display","none");
		$("#List2_312").css("display","none");
		$(".recently-added-products .homepage_buttons").css("display","none");
		return "";
		}else
		{
	
	if(document.getElementById)
	{return eval('document.getElementById("'+objName+'")')}
	else
	{return eval('document.all.'+objName)}
	}}
function AutoPlay_31(){
	clearInterval(AutoPlayObj_31);
	//AutoPlayObj_31=setInterval('ISL_GoDown_31();ISL_StopDown_31();',interval_31)
	}
function ISL_GoUp_31(){
	if(MoveLock_31)return;clearInterval(AutoPlayObj_31);
	MoveLock_31=true;MoveWay_31="left";
	MoveTimeObj_31=setInterval('ISL_ScrUp_31();',Speed_31);
	}
function ISL_StopUp_31(){
	if(MoveWay_31 == "right"){
		return
		};
		clearInterval(MoveTimeObj_31);
		if((GetObj3('ISL_Cont_31').scrollLeft-fill_31)%PageWidth_31!=0){
			Comp_31=fill_31-(GetObj3('ISL_Cont_31').scrollLeft%PageWidth_31);
			CompScr_31()
			}
			else{
				MoveLock_31=false
				}
	AutoPlay_31()
	}
function ISL_ScrUp_31(){
	if(GetObj3('ISL_Cont_31').scrollLeft<=0){
		GetObj3('ISL_Cont_31').scrollLeft=GetObj3('ISL_Cont_31').scrollLeft+GetObj3('List1_31').offsetWidth
		}
GetObj3('ISL_Cont_31').scrollLeft-=Space_31
}
function ISL_GoDown_31(){
	clearInterval(MoveTimeObj_31);
	if(MoveLock_31)return;
	clearInterval(AutoPlayObj_31);
	MoveLock_31=true;
	MoveWay_31="right";
	ISL_ScrDown_31();
	MoveTimeObj_31=setInterval('ISL_ScrDown_31()',Speed_31)
	}
function ISL_StopDown_31(){
	if(MoveWay_31 == "left"){
		return
		};clearInterval(MoveTimeObj_31);
		if(GetObj3('ISL_Cont_31').scrollLeft%PageWidth_31-(fill_31>=0?fill_31:fill_31+1)!=0){
			Comp_31=PageWidth_31-GetObj3('ISL_Cont_31').scrollLeft%PageWidth_31+fill_31;CompScr_31()
			}else{
				MoveLock_31=false
				}
AutoPlay_31()}
function ISL_ScrDown_31(){
	if(GetObj3('ISL_Cont_31').scrollLeft>=GetObj3('List1_31').scrollWidth){
		GetObj3('ISL_Cont_31').scrollLeft=GetObj3('ISL_Cont_31').scrollLeft-GetObj3('List1_31').scrollWidth
		}
GetObj3('ISL_Cont_31').scrollLeft+=Space_31
}
function CompScr_31(){
	if(Comp_31==0){
		MoveLock_31=false;
		return
		}
var num3,TempSpeed3=Speed_31,TempSpace3=Space_31;
if(Math.abs(Comp_31)<PageWidth_31/100){
	TempSpace3=Math.round(Math.abs(Comp_31/Space_31));
	if(TempSpace3<1){
		TempSpace3=1
		}}
if(Comp_31<0){
	if(Comp_31<-TempSpace3){
		Comp_31+=TempSpace3;num3=TempSpace3
		}else{
			num3=-Comp_31;Comp_31=0
			}
GetObj3('ISL_Cont_31').scrollLeft-=num3;setTimeout('CompScr_31()',TempSpeed3)}else{
	if(Comp_31>TempSpace3){
		Comp_31-=TempSpace3;num3=TempSpace3
		}else{
			num3=Comp_31;Comp_31=0
			}
GetObj3('ISL_Cont_31').scrollLeft+=num3;setTimeout('CompScr_31()',TempSpeed3)}}
function picrun_ini3(){
    GetObj3("List2_31").innerHTML=GetObj3("List1_31").innerHTML;
    GetObj3("List2_312").innerHTML=GetObj3("List1_31").innerHTML;
    GetObj3('ISL_Cont_31').scrollLeft=fill_31>=0?fill_31:GetObj3('List1_31').scrollWidth-Math.abs(fill_31);
    GetObj3("ISL_Cont_31").onmouseover=function(){clearInterval(AutoPlayObj_31)};
    GetObj3("ISL_Cont_31").onmouseout=function(){AutoPlay_31()};
    //AutoPlay_31();
}







$(document).ready(function () {

	var HomePageProducts_sizeB = $("#List1_1  .item-box").size();
	var bestsellers_sizeB = $("#List1_21  .item-box").size();
	var RecentlyAddedProducts_sizeB = $("#List1_31  .item-box").size();
	


	if (HomePageProducts_sizeB > 0) {
		$(".HomePageProducts_home").fadeIn(500);
		$(".RecentlyAddedProducts_home").hide();
		$(".BestSellers_home").hide();
		$(".HomePageProducts-button").css("background", "#02b3ff");
		$(".HomePageProducts-button").css("color", "#fff");
	}
	if (HomePageProducts_sizeB == 0 && RecentlyAddedProducts_sizeB > 0) {
		$(".RecentlyAddedProducts_home").fadeIn(500);
		$(".HomePageProducts_home").hide();
		$(".BestSellers_home").hide();
		$(".RecentlyAddedProducts-button").css("background", "#02b3ff");
		$(".RecentlyAddedProducts-button").css("color", "#fff");
	}
	if (HomePageProducts_sizeB == 0 && RecentlyAddedProducts_sizeB == 0 && bestsellers_sizeB > 0) {
		$(".BestSellers_home").fadeIn(500);
		$(".RecentlyAddedProducts_home").hide();
		$(".HomePageProducts_home").hide();
		$(".BestSellers-button").css("background", "#02b3ff");
		$(".BestSellers-button").css("color", "#fff");
	}
	if (HomePageProducts_sizeB > 0 && bestsellers_sizeB > 0 && HomePageProducts_sizeB > 0) {
		$(".HomePageProducts_home").fadeIn(500);
		$(".RecentlyAddedProducts_home").hide();
		$(".BestSellers_home").hide();
		$(".HomePageProducts-button").css("background", "#02b3ff");
		$(".HomePageProducts-button").css("color", "#fff");
	}


	if (HomePageProducts_sizeB == 0) {
		$(".HomePageProducts-button").css("display", "none");
	};

	if (bestsellers_sizeB == 0) {
		$(".BestSellers-button").css("display", "none");
	};
	if (RecentlyAddedProducts_sizeB == 0) {
		$(".RecentlyAddedProducts-button").css("display", "none");
	};

	if (RecentlyAddedProducts_sizeB == 0 && bestsellers_sizeB == 0 && HomePageProducts_sizeB == 0) {
		$(".homepage_products").css("display", "none");
	};



	$(".HomePageProducts-button").click(function () {
		$(".HomePageProducts_home").fadeIn(500);
		$(".RecentlyAddedProducts_home").hide();
		$(".BestSellers_home").hide();
		$(".HomePageProducts-button").css("background", "#02b3ff");
		$(".HomePageProducts-button").css("color", "#fff");
		$(".RecentlyAddedProducts-button").css("background", "#ededed");
		$(".BestSellers-button").css("background", "#ededed");
		$(".RecentlyAddedProducts-button").css("color", "#555");
		$(".BestSellers-button").css("color", "#555");
	});


	$(".RecentlyAddedProducts-button").click(function () {
		$(".RecentlyAddedProducts_home").fadeIn(500);
		$(".HomePageProducts_home").hide();
		$(".BestSellers_home").hide();

		$(".HomePageProducts-button").css("background", "#ededed");
		$(".HomePageProducts-button").css("color", "#555");

		$(".RecentlyAddedProducts-button").css("background", "#02b3ff");
		$(".RecentlyAddedProducts-button").css("color", "#fff");

		$(".BestSellers-button").css("background", "#ededed");
		$(".BestSellers-button").css("color", "#555");
	});


	$(".BestSellers-button").click(function () {
		$(".BestSellers_home").fadeIn(500);
		$(".HomePageProducts_home").hide();
		$(".RecentlyAddedProducts_home").hide();
		$(".HomePageProducts-button").css("background", "#ededed");
		$(".RecentlyAddedProducts-button").css("background", "#ededed");
		$(".BestSellers-button").css("background", "#02b3ff");
		$(".BestSellers-button").css("color", "#fff");
		$(".HomePageProducts-button").css("color", "#555");
		$(".RecentlyAddedProducts-button").css("color", "#555");
	});
});