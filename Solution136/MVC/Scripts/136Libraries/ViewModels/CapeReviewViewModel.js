function CapeReviewViewModel() {
    var CapeReviewModelObj = new CapeReviewModel();
    var self = this;
    var initialBind = true;
    var capereviewListViewModel = ko.observableArray();
  

    this.Initialize = function(sid, iname, cname) {

        var viewModel = {
            id: sid,
            instructor: ko.observable(iname),
            irating: ko.observable(5),
            cname: ko.observable(cname),
            crating: ko.observable("5"),
            reason: ko.observable("Reason for your request"),
            add: function (data) {
                self.AddCapeReview(data, sid);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divStudentEnrollments"));
    };

    this.AddCapeReview = function (data, student_id) {

        var model = {
            id: sid,
            instructor: ko.observable(iname),
            irating: ko.observable(5),
            cname: ko.observable(cname),
            crating: ko.observable("5"),
            reason: ko.observable("Reason for your request"),
         
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
