//==================================================================== مقدار دهی متغیر های عمومی =============================================
// ------------------------------------------------ متغیر کد بازاریابی معرف
var GloabalRegeantMK;
// ------------------------------------------------ متغیر کد بازاریابی یوزر
var GloabalLeaderMK;
//------------------------------------------------ متغیر مجموع حساب کاربر
var GlobalDepositsum = 0;
//------------------------------------------------ متغیر مجموع حساب کاربر
var GlobalCommision = 0;
//------------------------------------------------ متغیر کد کاربری ها
var UsersMK;
//------------------------------------------------ متغیر بازوها
var LevelArms;
//------------------------------------------------ متغیر کد کاربری ها
var UsersMKArray = new Array();
var GlobalCalcute1 = 0;
var GlobalCalcute2 = 0;
var GlobalCalcute3 = 0;
var GlobalCalcute4 = 0;
var GlobalCalcute5 = 0;
var GlobalCalcute6 = 0;
var GlobalCalcute7 = 0;
var GlobalCalcute8 = 0;
var GlobalCalcute9 = 0;
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن معرف +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function RegeantMK(MarketingCode) {
    $.ajax({
        type: 'Get',
        url: 'FindRegeantMK',
        async: false,
        data: { "MarketingCode": MarketingCode },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                //alert(data);
                GloabalRegeantMK = data;
            }
            if (data == null) {
                alert('معرف وجود ندارد');
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//==========================================================تابع ورود سود معرف  ========================================
function InsertCommisionRegeant(UWMarketingCodeFrom,UWMarketingCode, UWAmountDeposit) {

    $.ajax({
        type: "POST",
        url: "InsertCommisionRegeant",
        data: '{UWMarketingCode: "' + UWMarketingCode +
            '", UWMarketingCodeFrom: "' + UWMarketingCodeFrom +
        '", UWAmountDeposit: "' + UWAmountDeposit +

        '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            return true;
        }
    });
}
//==========================================================تابع ورود چک کردن سود  ========================================
function CheckCommision(UWMarketingCodeFrom, UWMarketingCode, UWDateWithoutPoints) {

    $.ajax({
        type: "POST",
        url: "CheckCommision",
        async: false,
        data: '{UWMarketingCode: "' + UWMarketingCode +
            '", UWMarketingCodeFrom: "' + UWMarketingCodeFrom +
            '", UWDateWithoutPoints: "' + UWDateWithoutPoints +
            '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status, x) {

                //alert("GloabalLeaderMK" + data);
                GlobalCommision = data;
                //alert(data);
                //alert(GlobalCommision);
            //if (data === null || data === undefined || data === "Null") {
            //    alert('سودها تخصیص یافت');
            //    return false;
            //}

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------
//==========================================================تابع ورود سود معرف  ========================================
function CalculateLeaderComission(AmountsArray) {
    alert("AmountArray" + AmountsArray);
    alert("AmountArray1" + AmountsArray[0]);
    //alert("AmountArray1" + AmountsArray);
    //var L1 = (AmountsArray[0] * 10) / 100;
    $.each([AmountsArray], function (index, value) {
        alert(index + ": " + value);
    });
    //var Calculate = parseFloat((AmountsArray[0] * 6) / 100 + (AmountsArray[1] * 4) / 100 + (AmountsArray[2] * 2) / 100 + (AmountsArray[3] * 2) / 100 + (AmountsArray[4] * 2) / 100 + (AmountsArray[5] * 2) / 100) + (AmountsArray[6] * 4) / 100 + (AmountsArray[7] * 6) / 100 + (AmountsArray[8] * 6) / 100;
    //alert("Calculate : " + Calculate);
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن معرف +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function FindLeaderMK(MarketingCode) {
    $.ajax({
        type: 'Get',
        url: 'FindLeaderMK',
        async: false,
        data: { "MarketingCode": MarketingCode },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                //alert("GloabalLeaderMK" + data);
                GloabalLeaderMK = data;
            }
            if (data === null || data === undefined || data === "Null") {
                alert('سودها تخصیص یافت');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//==========================================================تابع ورود سود معرف  ========================================
function InsertCommisionLeader(UWMarketingCodeFrom,UWMarketingCode, UWAmountDeposit) {

    $.ajax({
        type: "POST",
        url: "InsertCommisionRegeant",
        data: '{UWMarketingCode: "' + UWMarketingCode +
            '", UWMarketingCodeFrom: "' + UWMarketingCodeFrom +
        '", UWAmountDeposit: "' + UWAmountDeposit +

        '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            //alert("Ok");
            return true;
        }
    });
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ گزارشات ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//==========================================================تابع گزارش جمع درامد ماهیانه  ========================================
function GetMontlyReport(Year, Month) {
    $.ajax({
        type: 'Get',
        url: 'GetMontlyReport',
        async: false,
        data: {
            "Year": Year,
            "Month": Month
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                var RowCounter = 1;
                console.time('Diposit');
                $.each(data, function (id, userWallet) {
                    GlobalDepositsum = GlobalDepositsum + parseFloat(userWallet.UWAmountDeposit);

                    //alert("GloabalLeaderMK" + data);
                    //alert(userWallet.UWDayDeposit);
                    //alert(userWallet.UWMonthDeposit);
                    //alert(userWallet.UWYearDeposit);
                    //alert(userWallet.UWAmountDeposit);
                    $('#MonthlyReportHeader').after(/*'<tr id=row ' + id + '>' +*/
                        //'<td class="text-center" id="UWMarketingCode' + RowCounter +'">' + userWallet.UWMarketingCode + '</td>' +
                        //'<td class="text-center" id="UWYearDeposit' + RowCounter +'">' + userWallet.UWYearDeposit + '</td>' +
                        //'<td class="text-center" id="UWMonthDeposit' + RowCounter +'">'+userWallet.UWMonthDeposit+'</td>'+
                        //'<td class="text-center" id="UWDayDeposit' + RowCounter +'">' + userWallet.UWDayDeposit + '</td>' +
                        //'<td class="text-center Amount" id="UWAmountDeposit' + RowCounter +'">' + userWallet.UWAmountDeposit + "  ريال" + '</td>' +
                        //'</tr>' 

                    );

                    ToRial($('#UWAmountDeposit' + RowCounter + '').text(), $('#UWAmountDeposit' + RowCounter + '').attr('id'));
                    RowCounter++;
                    //ToRial($(".Amount").val());
                    //$(".Amount").each(function () {

                    //    if (!isNaN(value) && value.length != 0) {
                    //        sum += parseFloat(SumAmount);

                    //    }
                    //});
                    //alert($('#MonthlyReportHeader tr').text());
                });
                console.timeEnd('Diposit');
            }
            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------
//==========================================================تابع گزارش جمع زمانی درامد   ========================================
function GetPeriodlyReport(FromYear, FromMounth, ToYear, ToMounth) {
    $.ajax({
        type: 'Get',
        url: 'GetPeriodlyReport',
        async: false,
        data: {
            "FromYear": FromYear,
            "FromMounth": FromMounth,
            "ToYear": ToYear,
            "ToMounth": ToMounth
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                var RowCounter = 1;
                console.time('Diposit');
                $.each(data, function (id, userWallet) {
                    GlobalDepositsum = GlobalDepositsum + parseFloat(userWallet.UWAmountDeposit);

                    $('#MonthlyReportHeader').after(/*'<tr id=row ' + id + '>' +*/


                    );

                    ToRial($('#UWAmountDeposit' + RowCounter + '').text(), $('#UWAmountDeposit' + RowCounter + '').attr('id'));
                    RowCounter++;
                    //ToRial($(".Amount").val());
                    //$(".Amount").each(function () {

                    //    if (!isNaN(value) && value.length != 0) {
                    //        sum += parseFloat(SumAmount);

                    //    }
                    //});
                    //alert($('#MonthlyReportHeader tr').text());
                });
                console.timeEnd('Diposit');
            }
            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن معرف +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetArmsNum() {
    $.ajax({
        type: 'Get',
        url: '../FindArmsNum',
        async: false,
        data: {},
        dataType: 'json',

        success: function (data, status, x) {
            if (data !== null) {
                var RowCounter = 0;

                LevelArms = data;
            }
            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن معرف +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetMKLevel(LevelMK, UserId) {

    $.ajax({
        type: 'Get',
        url: '../FindUserMK',
        async: false,
        data: {
            "LevelMK": LevelMK,
            "UserId": UserId
        },
        dataType: 'json',

        success: function (data, status, x) {
            if (data !== null) {
                //خالی کردن آرایه
                UsersMKArray = [];
                var RowCounter = 0;

                UsersMK = data;
                $.each(data, function (id, userData) {

                    UsersMKArray.push(userData);
                });

            }
            if (data == null || data == "") {
                alert('در این سطح شما زیرگروه ندارید');
            }
            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//========================================================== تبدیل به مقدار پولی ========================================

function ToRial(str, id) {

    str = str.replace(/\,/g, '');


    var objRegex = new RegExp('(-?[0-9]+)([0-9]{3})');

    while (objRegex.test(str)) {
        str = str.replace(objRegex, '$1,$2');
        //alert(str);
        //alert(id);
    }
    $("#" + id).text(str);

    return str;
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن کد بازاریابی +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function FindMK(MK) {

    $.ajax({
        type: 'Get',
        url: 'FindMK',
        async: false,
        data: {
            "MK": MK
        },
        dataType: 'json',

        success: function (data, status, x) {
            if (data !== null) {
                //خالی کردن آرایه
                UsersMKStatusArray = [];
                var RowCounter = 0;

                UsersMK = data;
                $.each(data, function (id, userData) {

                    UsersMKStatusArray.push(userData);
                });

            }
            if (data == null || data == "") {
                alert('در این سطح شما زیرگروه ندارید');
            }
            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن کد بازاریابی +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function FindNewMK() {
    alert("FindNewMK");
    $.ajax({
        type: 'Get',
        url: 'FindNewMK',
        async: false,
        data: {
        },
        dataType: 'json',

        success: function (data, status, x) {
            if (data !== null) {
                //خالی کردن آرایه
                UsersMKStatusArray = [];
                var RowCounter = 0;

                UsersMK = data;
                $.each(data, function (id, userData) {

                    UsersMKStatusArray.push(userData);
                });

            }
            if (data == null || data == "") {
                alert('یوزر جدید وجود ندارد');
            }
            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ بدست آوردن کمیسیون اول +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetCommision1Level(Level) {

    $.ajax({
        type: 'Get',
        url: 'FindCommision1Level',
        async: false,
        data: {
            "Level": Level
        },
        dataType: 'json',

        success: function (data, status, x) {
            if (data !== null) {
                //خالی کردن آرایه
                //alert(data);
                GlobalCalcute1 = data;

            }
            //if (data == null || data == "") {
            //    alert('در این سطح شما زیرگروه ندارید');
            //}
            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ بدست آوردن کمیسیون دوم +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetCommision2Level(Level) {

    $.ajax({
        type: 'Get',
        url: 'FindCommision2Level',
        async: false,
        data: {
            "Level": Level
        },
        dataType: 'json',

        success: function (data, status, x) {
            if (data !== null) {
                //خالی کردن آرایه
                //alert(data);

                GlobalCalcute2 = data;
            }

            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ بدست آوردن کمیسیون دوم +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetCommision3Level(Level) {

    $.ajax({
        type: 'Get',
        url: 'FindCommision3Level',
        async: false,
        data: {
            "Level": Level
        },
        dataType: 'json',

        success: function (data, status, x) {
            if (data !== null) {
                //خالی کردن آرایه
                //alert(data);

                GlobalCalcute3 = data;
            }
            //if (data == null || data == "") {
            //    alert('در این سطح شما زیرگروه ندارید');
            //}
            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ بدست آوردن کمیسیون چهارم +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetCommision4Level(Level) {

    $.ajax({
        type: 'Get',
        url: 'FindCommision4Level',
        async: false,
        data: {
            "Level": Level
        },
        dataType: 'json',

        success: function (data, status, x) {
            if (data !== null) {
                //خالی کردن آرایه
                //(data);

                GlobalCalcute4 = data;
            }
            //if (data == null || data == "") {
            //    alert('در این سطح شما زیرگروه ندارید');
            //}
            if (data === null || data === undefined || data === "Null") {
                alert('Error');
                return false;
            }

        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
