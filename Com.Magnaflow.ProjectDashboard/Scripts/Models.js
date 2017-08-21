var Models = {};
Models.Project = {};
Models.Project.Add = function (name, requestor, department, status, dueDate, contact, summary, priority) {
    var Name = name,
        Requestor = requestor,
        Department = department,
        Status = status,
        DueDate = dueDate,
        Contact = contact,
        Summary = summary,
        Priority = priority;
    return {
        Name : Name,
        Requestor : Requestor,
        Request_Department : Department,
        Status : Status,
        Due_Date : DueDate,
        IT_Owner : Contact,
        Summary : Summary,
        Priority : Priority
    }
};