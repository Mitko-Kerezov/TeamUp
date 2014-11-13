function generateTemplate(data) {
    var template = "";
    if (data != null) {

        for (var i = 0; i < data.length; i++) {
            template += data[i] + " ";
        }
    }
    if (template === "") {
        template = "Hasn't specified";
    }
    return template;
}