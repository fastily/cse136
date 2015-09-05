//// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
//// Due to async nature of ajax, the Jasmine's compare function would throw an error during
//// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
//// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
//// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
//// be async when called by viewModel.
function GradeChangeModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.Create = function (gradechange, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/GradeChange/AddGradeChange",
            data: gradechange,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding gradechange.  Is your service layer running?');
            }
        });
    };

    this.Approve = function (gradechange, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/GradeChange/ApproveGradeChange",
            data: gradechange,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while updating gradechange.  Is your service layer running?');
            }
        });
    };
    
    this.GetAllByStudentId = function (id, callback) {
        var url = "http://localhost:9393/Api/GradeChange/FindGradeChangeByStudentId?idt=" + id;
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
                alert('Error while loading gradechange list.  Is your service layer running?');
            }
        });
    };

    this.GetAllByCourseId = function (id, callback) {
        var url = "http://localhost:9393/Api/GradeChange/FindGradeChangeByCourseId?idt=" + id;
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
                alert('Error while loading gradechange list.  Is your service layer running?');
            }
        });
    };

    this.GetScheduleID= function (id, callback) {
        var url = "http://localhost:9393/Api/GradeChange/GetGradeChangeScheduleId?sid=" + sid + "&cid=" + cid;
            
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
                alert('Error while loading gradechange detail.  Is your service layer running?');
            }
        });
    };
}
