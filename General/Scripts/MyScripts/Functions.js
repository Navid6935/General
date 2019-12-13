//==================================================================== مقدار دهی متغیر های عمومی =============================================
var GlobalUserNumInLvl;
var GlobalLeaderMK;
var GlobalReagentMK;
var GlobalUserRegister;
var GlobalUserArms;
var GlobalArms;
var GlobalUsersNumArm;
var GlobalCounter;
var GlobalLastMK;
var GlobalUnreadMessages;
var GlobalSumCommisions;
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function makeid() {
    var text = "";
    var UpperLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var LowerLetter = "abcdefghijklmnopqrstuvwxyz";
    var Numbers = "123456789";
    var Charachter = "!#@$%&";
    for (var i = 0; i < 1; i++) {
        text += text += UpperLetter.charAt(Math.floor(Math.random() * UpperLetter.length));
        text += text += LowerLetter.charAt(Math.floor(Math.random() * LowerLetter.length));
        text += text += Numbers.charAt(Math.floor(Math.random() * Numbers.length));

        text += text += Charachter.charAt(Math.floor(Math.random() * Charachter.length));


    }

    return text;
}
function CheckFill() {
    alert("لطفاً ورودی ها را تکمیل نمایید");
    //$(id).css("border-color", "red");
    //$(id).css("background-color", "#fdcdcd");

}
function istruefunction(id) {
    id.focusout(function () {
        if ($(this).val() !== "") {
            $(this).css("background-color", "white");
            $(this).css("border-color", "#cccccc");
        }
    });
}
//++++++++++++++++++++++++++++++++++++++++++++++++ گرفتن آخرین کد بازاریابی در لول ارسالی ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function GetLastCode(Level) {
    $.ajax({
        type: 'Get',
        url: '../FindMarketingCode',
        async: false,
        data: { "Level": Level },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                alert(data);
                alert(parseInt(data.slice(6, 13)) + 1);
                GlobalUserNumInLvl = parseInt(data.slice(6, 13)) + 1;
            }
            if (data === null) {
                alert('Error');
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

//++++++++++++++++++++++++++++++++++++++++++++++++ گرفتن آخرین کد بازاریابی در لول ارسالی ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function GetLevel(LeaderMK) {
    $.ajax({
        type: 'Get',
        url: '../FindLastLevel',
        async: false,
        data: { "Level": Level },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                alert(data);
                alert(parseInt(data.slice(6, 13)) + 1);
                GlobalUserNumInLvl = parseInt(data.slice(6, 13)) + 1;
            }
            if (data === null) {
                alert('Error');
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
//++++++++++++++++++++++++++++++++++++++++++++++++ گرفتن آخرین کد بازاریابی در لول ارسالی ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function GetArms(GlobalLeaderMK) {
    $.ajax({
        type: 'Get',
        url: '../FindArms',
        async: false,
        data: { "LeaderMk": GlobalLeaderMK },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                alert(data);
                GlobalUsersNumArm = data;
                alert("GlobalUsersNumArm : " + GlobalUsersNumArm);

            }
            if (data === null) {
                alert('Error');
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
//++++++++++++++++++++++++++++++++++++++++++++++++ تابع درج کد بازاریابی ++++++++++++++++++++++++++++++++++++++++++++++++++++++
//function InsertMarketingCode(UserName) {
//    $.ajax({
//        type: "POST",
//        url: "../InsertMarketingCode",
//        data: '{UserName:' + UserName + '}',
//        contentType: "application/json; charset=utf-8",
//        dataType: "json"
//    });
//});
//++++++++++++++++++++++++++++++++++++++++++++++++ تابع درج کد بازاریابی سرگروه ++++++++++++++++++++++++++++++++++++++++++++++++++++++

function InsertLeaderMarketingCode(UserName, Level, LeadersMarketingCode) {


    $.ajax({
        type: "POST",
        url: "../InsertLeaderMCAndLevel",
        data: '{username: "' + UserName +
            '",level: "' + Level +
            '", leadermc: "' + LeadersMarketingCode + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            alert(r.data);
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
//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++ تابع درج کد بازاریابی معرف ++++++++++++++++++++++++++++++++++++++++++++++++++++++

function InsertReagentMarketingCode(UserName, Level, MarketingCode) {
    alert(UserName);
    alert(Level);
    alert(LeadersMarketingCode);

    $.ajax({
        type: "POST",
        url: "../InsertLeaderMCAndLevel",
        data: '{username: "' + UserName +
            '",level: "' + Level +
            '", leadermc: "' + LeadersMarketingCode + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            alert(r.data);
            alert("oK");

            return true;
        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert("Error" + jqXHR + textStatus + errorThrown);
        }
    });
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++ تابع چک کردن کد  ++++++++++++++++++++++++++++++++++++++++++++++++++++++


function CheckMarketingCode(MarketingCode) {
    $.ajax({
        type: 'GET',
        url: "../CkeckMarketingCode",
        data: { "MarketingCode": MarketingCode },
        async: false,
        dataType: 'json',
        success: function (data, status, x) {

            if (data !== null) {
                GlobalLeaderMK = data;
            }
            if (data === null || data === undefined || x.status !== 200) {
                alert("Data is Empty");
            }
        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown, data) {
        }
    });
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++ تابع چک کردن کد  ++++++++++++++++++++++++++++++++++++++++++++++++++++++


function CheckReagentMarketingCode(MarketingCode) {
    $.ajax({
        type: 'GET',
        url: "../CkeckMarketingCode",
        data: { "MarketingCode": MarketingCode },
        async: false,
        dataType: 'json',
        success: function (data, status, x) {

            if (data !== null) {

                GlobalReagentMK = data;
            }
            if (data === null || data === undefined || x.status !== 200) {
                alert("Data is Empty");
            }
        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown, data) {
        }
    });
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++ تابع پیدا کردن مشخصات یوزر  ++++++++++++++++++++++++++++++++++++++++++++++++++++++


function FindUserStatus(Id) {
    $.ajax({
        type: 'GET',
        url: "../FindUserStatus",
        data: { "Id": Id },
        dataType: 'json',
        success: function (data, status, x) {

            if (data !== null) {
                $("#MarketingCode").val(data.MarketingCode);
                $("#InsuranceNumber").val(data.InsuranceNumber6 + " " + data.InsuranceNumber5 + " " + data.InsuranceNumber4 + " " + data.InsuranceNumber4 + " " + data.InsuranceNumber2 + " " + data.InsuranceNumber1);
                $("#FirstName").val(data.FirstName);
                $("#FirstName").addClass("text-center");
                $("#LastName").val(data.LastName);
                $("#LastName").addClass("text-center");
                $("#MarketingCode").prop("disabled", true);
                $("#MarketingCode").addClass("text-center");
                $("#InsuranceNumber").prop("disabled", true);
                $("#InsuranceNumber").addClass("text-center");
                $("#FirstName").prop("disabled", true);
                $("#LastName").prop("disabled", true);
                $("#MarketingCode").css("color", "black");
                $("#InsuranceNumber").css("color", "black");
                $("#FirstName").css("color", "black");
                $("#LastName").css("color", "black");

            }
            if (data === null || data === undefined || x.status !== 200) {
                alert("Data is Empty");
            }
        },
        beforeSend: function () {
        },
        complete: function () {
        },
        error: function (jqXHR, textStatus, errorThrown, data) {
            alert(Error);
        }
    });
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------
//==========================================================تابع ورود اطلاعات اقساط  ========================================
function InsertInstallmentData(MarketingCode, FirstName, LastName, InsuranceNumber, InstallmentsNumbersAmount, InstallmentsNumbersNum, InstallmentsNumberId,
    DayInstallmentNumber, MounthInstallmentNumber, YearInstallmentNumber, InstallmentsNumberStatus) {

    $.ajax({
        type: "POST",
        url: "../InsertInstallmentData",
        data: '{MarketingCode: "' + MarketingCode +
            '", FirstName: "' + FirstName +
            '", LastName: "' + LastName +
            '", InsuranceNumber: "' + InsuranceNumber +
            '", InstallmentsNumbersAmount: "' + InstallmentsNumbersAmount +
            '" , InstallmentsNumbersNum: "' + InstallmentsNumbersNum +
            '", InstallmentsNumberId: "' + InstallmentsNumberId +
            '", DayInstallmentNumber: "' + DayInstallmentNumber +
            '", MounthInstallmentNumber: "' + MounthInstallmentNumber +
            '", YearInstallmentNumber: "' + YearInstallmentNumber +
            '", InstallmentsNumberStatus: "' + InstallmentsNumberStatus +

            '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            // window.location.href = '/Administrator/Installments/Index/' + MarketingCode +'';.

            swal({
                title: "موفق!",
                text: "اقساط برای آقای / خانم " + FirstName + " " + LastName + " اضافه گردید",
                type: "success"
            }).then(function () {
                window.location = "/Administrator/UsersAdmin/";
            });

            return true;
        }
    });
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------
//========================================================== تبدیل به مقدار پولی ========================================

function ToRial(str, id) {

    str = str.replace(/\,/g, '');
    var objRegex = new RegExp('(-?[0-9]+)([0-9]{3})');

    while (objRegex.test(str)) {
        str = str.replace(objRegex, '$1,$2');
    }
    $("#" + id).val(str);

    return str;
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------

//++++++++++++++++++++++++++++++++++++++++++++++++ چک کردن کد کپچا ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function CheckCaptcha(FirstCaptchaVal) {
    $.ajax({
        type: 'Get',
        url: '../CheckCaptcha',
        async: false,
        data: { "FirstCaptchaVal": FirstCaptchaVal },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                alert(data);
            }
            if (data === null) {
                alert('Error');
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

//++++++++++++++++++++++++++++++++++++++++++++++++ چک کردن  یوزر ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function CheckUserRegister(UserId) {
    $.ajax({
        type: 'Get',
        url: '../CheckUserRegister',
        async: false,
        data: { "UserId": UserId },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                GlobalUserRegister = data;
            }
            if (data === null) {
                alert('Error');
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

//++++++++++++++++++++++++++++++++++++++++++++++++ چک کردن کد کپچا ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function CheckUserArms(LeaderMK) {
    $.ajax({
        type: 'Get',
        url: '../CheckUserArms',
        async: false,
        data: { "LeaderMK": LeaderMK },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                //alert(data);
                GlobalUserArms = data;
            }
            if (data === null) {
                alert('Error');
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

//++++++++++++++++++++++++++++++++++++++++++++++++ چک کردن کد کپچا ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function ArmsNum() {
    $.ajax({
        type: 'Get',
        url: '../ArmsNum',
        async: false,
        data: { "": "" },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                GlobalArms = data;
            }
            if (data === null) {
                alert('Error');
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

//++++++++++++++++++++++++++++++++++++++++++++++++ چک کردن کد کپچا ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function GetLastCounter() {
    $.ajax({
        type: 'Get',
        url: '../GetLastCounter',
        async: false,
        data: { "": "" },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                alert(data);
                GlobalCounter = data;
            }
            if (data === null) {
                alert('Error');
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

//++++++++++++++++++++++++++++++++++++++++++++++++ ثبت یوزر ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function UpdateUserData(Id, FinalMK) {
    //GetLevel(GlobalLeaderMK);
    //alert("PId : " + Id);
    //alert(FinalMK);
    var UserData = {};
    UserData.UserName = Id;
    UserData.FirstName = $("#FirstName").val();
    UserData.LastName = $("#LastName").val();
    UserData.FullName = $("#FirstName").val() + " " + $("#LastName").val();
    UserData.FathersName = $("#FathersName").val();
    UserData.DayofBirth = $("#DayofBirth").val();
    UserData.MounthofBirth = $("#MounthofBirth").val();
    UserData.YearofBirth = $("#YearofBirth").val();
    UserData.BirthCertificateId = $("#BirthCertificateId").val();
    UserData.EId = $("#EId").val();
    UserData.Sex = $("#Sex").val();
    UserData.Phone = $("#Phone").val();
    UserData.MobileNumber = $("#MobileNumber").val();
    UserData.Email = $("#Email").val();
    UserData.CId = $("#CId").val();
    UserData.SId = $("#SId").val();
    UserData.PostalCode = $("#PostalCode").val();
    UserData.Address = $("#Address").val();
    UserData.AcountNumber = $("#AcountNumber").val();
    UserData.PropductModelCode = $("#PropductModelCode").val();
    UserData.AcountNumber = $("#AcountNumber").val();
    UserData.CardNumber = $("#CardNumber").val();
    UserData.BranchName = $("#BranchName").val();
    UserData.BranchCode = $("#BranchCode").val();
    UserData.ShabaName = $("#ShabaName").val();
    UserData.InsuranceNumber1 = $("#InsuranceNumber1").val();
    UserData.InsuranceNumber2 = $("#InsuranceNumber2").val();
    UserData.InsuranceNumber3 = $("#InsuranceNumber3").val();
    UserData.InsuranceNumber4 = $("#InsuranceNumber4").val();
    UserData.InsuranceNumber5 = $("#InsuranceNumber5").val();
    UserData.InsuranceNumber6 = $("#InsuranceNumber6").val();
    UserData.MarketingCode = FinalMK;
    UserData.LeadersMarketingCode = $("#LeadersMarketingCode").val();
    UserData.ReagentMarketingCode = $("#ReagentMarketingCode").val();
    UserData.Password2 = $("#Password2").val();
    UserData.ConfirmPassword2 = $("#ConfirmPassword2").val();
    UserData.ArmsNum = GlobalUsersNumArm;
    UserData.RegiterStatus = 1;
    //alert("UserData : " + JSON.stringify(UserData));
    //alert("UserData : " + UserData.FirstName);
    //alert("UserData : " + UserData.LastName);
    //alert("UserData : " + UserData.MarketingCode);

    $.ajax({
        type: "POST",
        url: "../UpdateUserData",
        data: '{UserData:' + JSON.stringify(UserData) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++ بدست آوردن آخرین کد در رنچ ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function FindLastMKInRage(MK) {
    //alert("FindLastMKInRage");
    $.ajax({
        type: 'Get',
        url: '../GetLastMKInRage',
        async: false,
        data: { "marketingcode": MK },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                ;
                //alert("dataLenght : " + data.length);
                //====================================== ساختن کد بازاریابی


                var IntMK = data.slice(6, 13);
                var MKPlus = parseInt(IntMK) + 1;
                GlobalLastMK = "000000" + MKPlus;
            }
            if (data === null) {
                alert('Error');
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

//++++++++++++++++++++++++++++++++++++++++++++++++ ثبت یوزر ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function UpdateUserCoopertionStatus(PID, MK, DisconnectCooperationCause) {
    //alert("PId : " + PID);
    ;
    alert(DisconnectCooperationCause);

    $.ajax({
        type: "POST",
        url: "../UpdateUserCoopertionStatusDisconnect",
        data: '{PID: "' + PID +
            '", MK: "' + MK +
            '", DisconnectCooperationCause: "' + DisconnectCooperationCause +
            '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status, x) {
            alert("یوزر غیرفعال گردید");
            window.location.href = '/Administrator/UsersAdmin/Index/';

        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++ فعال کردن یوزر ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function UpdateUserCoopertionStatusConnected(PID, MK) {
    //alert("PId : " + PID);
    //alert(MK);

    $.ajax({
        type: "POST",
        url: "../UpdateUserCoopertionStatusConnect",
        data: '{PID: "' + PID +
            '", MK: "' + MK +
            '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status, x) {
            alert("یوزر فعال گردید");
            window.location.href = '/Administrator/UsersAdmin/Index/';

        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert(jqXHR + textStatus + errorThrown);
        }
    });
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetInstallmentList(FromDate, ToDate) {
    $.ajax({
        type: 'Get',
        url: 'GetInstallmentList',
        async: false,
        data: {
            "FromDate": FromDate,
            "ToDate": ToDate
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                //alert("GloabalLeaderMK" + data);

                InstallmentDataArray = [];
                $.each(data, function (id, InstallmentData) {

                    InstallmentDataArray.push(InstallmentData);
                });


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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها  بر اساس کد بازاریابی +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetInstallmentListOnMarketingCode(MarketingCode) {
    $.ajax({
        type: 'Get',
        url: 'GetInstallmentListOnMarketingCode',
        async: false,
        data: {
            "MarketingCode": MarketingCode,
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                //alert("GloabalLeaderMK" + data);

                InstallmentDataArray = [];
                $.each(data, function (id, InstallmentData) {

                    InstallmentDataArray.push(InstallmentData);
                });


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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++  پیدا کردن لیست قسط ها بر اساس شماره بیمه نامه +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetInstallmentListOnInsuranceNum(InsuranceNum) {
    $.ajax({
        type: 'Get',
        url: 'GetInstallmentListOnInsuranceNum',
        async: false,
        data: {
            "InsuranceNum": InsuranceNum,
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                //alert("GloabalLeaderMK" + data);

                InstallmentDataArray = [];
                $.each(data, function (id, InstallmentData) {

                    InstallmentDataArray.push(InstallmentData);
                });


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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetExcelPaymentOnPeriod(FromDate, ToDate) {

    $.ajax({
        type: 'Get',
        url: 'GetExcelPaymentOnPeriod',
        async: false,
        data: {
            "FromDate": FromDate,
            "ToDate": ToDate
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                InstallmentonPeriodArray = [];
                $.each(data, function (id, Installment) {

                    InstallmentonPeriodArray.push(Installment);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetExcelPaymentOnPeriodInIndex(FromDate, ToDate) {

    $.ajax({
        type: 'Get',
        url: '../GetExcelPaymentOnPeriodInIndex',
        async: false,
        data: {
            "FromDate": FromDate,
            "ToDate": ToDate
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                InstallmentonPeriodArrayInIndex = [];
                $.each(data, function (id, Installment) {

                    InstallmentonPeriodArrayInIndex.push(Installment);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها  بر اساس کد بازاریابی+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetExcelPaymentOnPeriodOnMarketingCode(MarketingCode) {

    $.ajax({
        type: 'Get',
        url: 'GetExcelPaymentOnPeriodOnMarketingCode',
        async: false,
        data: {
            "MarketingCode": MarketingCode
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                InstallmentonPeriodArray = [];
                $.each(data, function (id, Installment) {

                    InstallmentonPeriodArray.push(Installment);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها  بر اساس کد بازاریابی+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetExcelPaymentOnPeriodOnMarketingCodeInIndex(MarketingCode) {

    $.ajax({
        type: 'Get',
        url: '../GetExcelPaymentOnPeriodOnMarketingCodeInIndex',
        async: false,
        data: {
            "MarketingCode": MarketingCode
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                InstallmentonPeriodArrayInIndex = [];
                $.each(data, function (id, Installment) {

                    InstallmentonPeriodArrayInIndex.push(Installment);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetExcelPaymentOnPeriodGetExcelPaymentOnPeriod(FromDate, ToDate) {

    $.ajax({
        type: 'Get',
        url: 'GetExcelPaymentOnPeriod',
        async: false,
        data: {
            "FromDate": FromDate,
            "ToDate": ToDate
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                InstallmentonPeriodArray = [];
                $.each(data, function (id, Installment) {

                    InstallmentonPeriodArray.push(Installment);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetExcelPaymentOnPeriodGetExcelPaymentOnPeriod(FromDate, ToDate) {

    $.ajax({
        type: 'Get',
        url: 'GetExcelPaymentOnPeriod',
        async: false,
        data: {
            "FromDate": FromDate,
            "ToDate": ToDate
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                InstallmentonPeriodArray = [];
                $.each(data, function (id, Installment) {

                    InstallmentonPeriodArray.push(Installment);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها  بر اساس کد بازاریابی+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetExcelPaymentOnPeriodOnInsuranceNum(InsuranceNum) {

    $.ajax({
        type: 'Get',
        url: 'GetExcelPaymentOnPeriodOnInsuranceNum',
        async: false,
        data: {
            "InsuranceNum": InsuranceNum
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                InstallmentonPeriodArray = [];
                $.each(data, function (id, Installment) {

                    InstallmentonPeriodArray.push(Installment);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست قسط ها  بر اساس شماره بیمه+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetListOnPeriodOnInsuranceNumInIndex(InsuranceNum) {

    $.ajax({
        type: 'Get',
        url: '../GetListInstallmentOnPeriodOnInsuranceNumberInIndex',
        async: false,
        data: {
            "InsuranceNum": InsuranceNum
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                InstallmentonPeriodArrayInIndex = [];
                $.each(data, function (id, Installment) {

                    InstallmentonPeriodArrayInIndex.push(Installment);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست پورسانت های پرداختی با بازه زمانی +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetAllWhole(FromDate, ToDate) {
    $.ajax({
        type: 'Get',
        url: 'GetAllWholes',
        async: false,
        data: {
            "FromDate": FromDate,
            "ToDate": ToDate
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                AllWholePaymentArray = [];
                $.each(data, function (id, AllWholeData) {

                    AllWholePaymentArray.push(AllWholeData);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست پورسانت های پرداختی با بازه زمانی +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetSumCommisions(MarketingCode) {
    $.ajax({
        type: 'Get',
        url: 'GetSumofCommision',
        async: false,
        data: {
            "MarketingCode": MarketingCode
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                GlobalSumCommisions = data;
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

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست پورسانت های پرداختی با کدلیست و شماره پیگیری +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetAllWholeOnListCodeAndFolowUpNo(ListCode, FollowUpNO) {
    $.ajax({
        type: 'Get',
        url: 'GetAllWholeOnListCodeAndFolowUpNo',
        async: false,
        data: {
            "ListCode": ListCode,
            "FollowUpNO": FollowUpNO
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                AllWholePaymentOnListCodeArray = [];
                $.each(data, function (id, AllWholeDataOnListCode) {

                    AllWholePaymentOnListCodeArray.push(AllWholeDataOnListCode);
                });
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

//++++++++++++++++++++++++++++++++++++++++++++++++ ثبت یوزر ++++++++++++++++++++++++++++++++++++++++++++++++++++++
function UpdateAllWholePayment(AllWholePaymentArray) {
    //GetLevel(GlobalLeaderMK);
    //alert(AllWholePaymentArray[0]);
    //alert(AllWholePaymentArray);
    var AllWholePayment = {};
    AllWholePayment.UWMarketingCode = AllWholePaymentArray[0];
    //AllWholePayment.UWAcountNumber = AllWholePaymentArray[1];
    //AllWholePayment.UWCardNumber = AllWholePaymentArray[2];
    //AllWholePayment.UWDayDeposit = AllWholePaymentArray[3];
    //AllWholePayment.UWMonthDeposit = AllWholePaymentArray[4];
    //AllWholePayment.UWYearDeposit = AllWholePaymentArray[5];
    //AllWholePayment.UWAmountDeposit = AllWholePaymentArray[6];
    AllWholePayment.UWId = AllWholePaymentArray[9];

    AllWholePayment.ListCode = AllWholePaymentArray[10];
    AllWholePayment.FollowUpNO = AllWholePaymentArray[11];
    AllWholePayment.UWDayPeyment = AllWholePaymentArray[12];
    AllWholePayment.UWMonthPeyment = AllWholePaymentArray[13];
    AllWholePayment.UWYearPeyment = AllWholePaymentArray[14];



    $.ajax({
        type: "POST",
        url: "UpdateAllWholePayment",
        data: '{userwallet:' + JSON.stringify(AllWholePayment) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست پورسانت های پرداختی با بازه زمانی +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetAllWholePeymented(FromYear, FromMounth, ToYear, ToMounth) {
    //alert(FromYear);
    $.ajax({
        type: 'Get',
        url: 'GetAllWholePeymented',
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

                AllWholePaymentArray = [];
                $.each(data, function (id, AllWholeData) {

                    AllWholePaymentArray.push(AllWholeData);
                });
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
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ پیدا کردن لیست پورسانت های پرداختی با بازه زمانی +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetAllWholePeymentedOnMK(MarketingCode) {
    //
    $.ajax({
        type: 'Get',
        url: 'GetAllWholePeymentedOnMarketingCode',
        async: false,
        data: {
            "MarketingCode": MarketingCode
        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {

                AllWholePaymentArray = [];
                $.each(data, function (id, AllWholeData) {

                    AllWholePaymentArray.push(AllWholeData);
                });
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
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ تعداد پیغام های خوانده نشده +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

function GetMessagesNotRead() {
    //
    $.ajax({
        type: 'Get',
        url: '../../../Users/Supports/GetMessagesNotRead',
        async: false,
        data: {

        },
        dataType: 'json',
        success: function (data, status, x) {
            if (data !== null) {
                if (data == undefined) {
                    GlobalUnreadMessages = 0;
                }
                GlobalUnreadMessages = data;
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
