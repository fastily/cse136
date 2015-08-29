function AdminViewModel() {
    var adminModelObj = new AdminModel();
    var self = this;

    this.Load = function (id) {
        // Because the Load() is a async call (asynchronous), we'll need to use
        // the callback approach to handle the data after data is loaded.
        adminModelObj.Load(id, function (result) {

            var viewModel = {
                first: ko.observable(result.FirstName),
                last : ko.observable(result.LastName),
                id: result.Id,
                update: function() {
                    self.UpdateAdmin(this);
                }
            }

            ko.applyBindings(viewModel , document.getElementById("divAdminEdit"));
        });
    };

    this.UpdateAdmin = function (viewModel) {
        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var adminData = {
            Id: viewModel.id,
            FirstName: viewModel.first(),
            LastName: viewModel.last()
        };

        adminModelObj.Update(adminData, function (message) {
            $('#divMessage').html(message);
        });

    };
}
