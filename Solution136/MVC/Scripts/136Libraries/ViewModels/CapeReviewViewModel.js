function CapeReviewViewModel() {
    var CapeReviewModelObj = new CapeReviewModel();
    var self = this;
    var initialBind = true;
    var capereviewListViewModel = ko.observableArray();
  

    this.Initialize = function(sid, inst, cname, cid) {

        var viewModel = {
            cname: ko.observable(cname),
            irating: ko.observable(), 
            crating: ko.observable(),
            reason: ko.observable("Reason for your request"),
            add: function (data) {
                self.AddCapeReview(data, sid, inst, cname, cid);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divStudentEnrollments"));
    };

    this.AddCapeReview = function (data, sid, inst, cname, cid) {

        var model = {
            cape_id: "1",
            instructor: inst,
            cid: cid,
            irating: data.irating(),
            reason: data.reason(),
            crating: data.crating()
         
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
