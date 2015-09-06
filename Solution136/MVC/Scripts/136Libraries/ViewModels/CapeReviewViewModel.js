function CapeReviewViewModel() {
    var CapeReviewModelObj = new CapeReviewModel();
    var self = this;
    var initialBind = true;
    var capereviewListViewModel = ko.observableArray();
  

    this.Initialize = function(sid, inst, cname, cid) {

        var viewModel = {
            cname: ko.observable(cname),
            irating: ko.observable("5"), 
            crating: ko.observable("5"),
            reason: ko.observable("Reason for your request"),
            add: function (data) {
                self.AddCapeReview(data, sid, inst, cid);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divStudentEnrollments"));
    };

    this.AddCapeReview = function (data, sid, inst, cid) {

        var model = {
            CapeId: "1",
            InstructorId: inst,
            CourseId: cid,
            InstructorRating: data.irating(),
            Summary: data.reason(),
            CourseRating: data.crating()
         
        };

        CapeReviewModelObj.Create(model, function(result) {
            if (result == "ok") {
                alert("Create CapeReview successful");
            } else {
                alert("Error creating CapeReview occurred");
            }
        });

    };

}
