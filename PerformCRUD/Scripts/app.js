



var ViewModel = function () {
    var self = this;
    self.studentLists = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();

    self.newStudent = {
        Name: ko.observable(),
        Age: ko.observable(),
        Grade: ko.observable()
    }

    self.existingStudent = {
        Id: ko.observable(),
        Name: ko.observable(),
        Age: ko.observable(),
        Grade: ko.observable()
    }

    var studentListsUri = '/api/StudentLists/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllStudents() {
        ajaxHelper(studentListsUri, 'GET').done(function (data) {
            self.studentLists(data);
        });
    }

    // Fetch the initial data.
    getAllStudents();

    //Function to perform GET Operation for selected student
    self.getStudentDetail = function (item) {
        ajaxHelper(studentListsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    //Function to perform ADD Operation  
    self.addStudent = function (formElement) {
        var student = {
            Name: self.newStudent.Name(),
            Age: self.newStudent.Age(),
            Grade: self.newStudent.Grade()
        };

        ajaxHelper(studentListsUri, 'POST', student).done(function (item) {
            self.studentLists.push(item);
            getAllStudents();
            alert("Record Added Successfully");
        });
    }

    //Function to perform DELETE Operation  
    self.deleteStudentDetail = function (item) {
        ajaxHelper(studentListsUri + item.Id, 'DELETE').done(function (data) {
            alert("Record Deleted Successfully");
            getAllStudents();
        });
    };

    //Function to perform UPDATE Operation  
    self.updateStudentDetail = function (item) {
        self.existingStudent.Id(item.Id);
        self.existingStudent.Name(item.Name);
        self.existingStudent.Age(item.Age);
        self.existingStudent.Grade(item.Grade);
    }

    self.updateStudent = function (item) {
        var student = {
            Id: self.existingStudent.Id(),
            Name: self.existingStudent.Name(),
            Age: self.existingStudent.Age(),
            Grade: self.existingStudent.Grade()
        };

        ajaxHelper(studentListsUri + self.existingStudent.Id(), 'PUT', student).done(function (item) {
            alert("Record Updated Successfully");
            getAllStudents();
        });
    }

};

ko.applyBindings(new ViewModel());