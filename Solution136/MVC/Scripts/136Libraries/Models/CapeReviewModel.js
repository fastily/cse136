//// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
//// Due to async nature of ajax, the Jasmine's compare function would throw an error during
//// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
//// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
//// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
//// be async when called by viewModel.
function CapeReviewModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.Create = function (caprereview, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/CapeReview/InsertCapeReview",
            data: caprereview,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding caprereview.  Is your service layer running?');
            }
        });
    };

    this.Approve = function (caprereview, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/CapeReview/ApproveCapeReview",
            data: caprereview,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while updating caprereview.  Is your service layer running?');
            }
        });
    };
    
    this.GetAllByStudentId = function (id, callback) {
        var url = "http://localhost:9393/Api/CapeReview/FindCapeReviewByStudentId?id=" + id;
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: id,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading caprereview list.  Is your service layer running?');
            }
        });
    };

    this.GetAllByCourseId = function (id, callback) {
        var url = "http://localhost:9393/Api/CapeReview/FindCapeReviewByCourseId?cid=" + id;
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: id,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading caprereview list.  Is your service layer running?');
            }
        });
    };

    this.GetScheduleID= function (id, callback) {
        var url = "http://localhost:9393/Api/CapeReview/GetCapeReviewScheduleId?sid=" + sid + "&cid=" + cid;
            
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: { sid: sid, cid: cid },
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading caprereview detail.  Is your service layer running?');
            }
        });
    };
}
