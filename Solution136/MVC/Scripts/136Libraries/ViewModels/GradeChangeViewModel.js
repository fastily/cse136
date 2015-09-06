function GradeChangeViewModel() {
    var GradeChangeModelObj = new GradeChangeModel();
    var self = this;
    var initialBind = true;
    var gradechangeListViewModel = ko.observableArray();
  

    this.Initialize = function(student_id) {

        var viewModel = {
            Studnet_id: student_id,
            id: ko.observable(1),
            course: ko.observable("CSE 3"),
            desired: ko.observable("A+"),
            reason: ko.observable("Reason for your request"),
            add: function (data) { 
                self.AddGradeChange(data, student_id);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divStudent"));
    };

    this.AddGradeChange = function (data, student_id) {

        var model = {
            /*
            GradeChangeId: 1,
            Student_id: "1",
            Schedule_id: 1,
            Approved: false,
            Course_id: 1,
            Desired: "A-",
            Description: "fffffffffffffffffuuuuuu"
           */
     
            GradeChangeId: data.id(),
            Student_id: student_id,
            Schedule_id: 1, //data.Schedule_id,
            Approved: false,
            Course_id: "1", //data.Course_id,
            Desired: data.desired(),
            Description: data.reason()
        }

        GradeChangeModelObj.Create(model, function(result) {
            if (result == "ok") {
                alert("Create GradeChange successful");
            } else {
                alert("Error creating GradeChange occurred");
            }
        });

    };


    this.Approve = function (gradechange) {
        var GradeChangeModelObj = new GradeChangeModel();

        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var gradechangeData = {
            Approved: true
        };

        GradeChangeModelObj.Approve(gradechangeData, function (message) {
            $('#divMessage').html(message);
        });

    };
}
