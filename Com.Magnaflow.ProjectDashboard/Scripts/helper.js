(function () {
    var _helper = function () {
        this.parseJSON = function (data) {
            ///<summary>Parse Web API generated JSON format</summary>
            ///<param name="data" type="object">
            ///<returns type="object">Parsed data object</returns>

            //Internal recursive function to traverse object tree
            var traverseTree = function (obj) {
                var prop;
                for (prop in obj)
                    if (obj.hasOwnProperty(prop)) {

                        if (obj[prop] === null || obj[prop] === undefined)
                            continue;

                        if (typeof obj[prop] === 'string')
                            if (/\d\d\d\d-\d\d-\d\dT\d\d:\d\d:\d\d/g.test(obj[prop]))
                                obj[prop] = new Date(Date.parse(obj[prop]));

                        if (typeof obj[prop] === 'object')
                            traverseTree(obj[prop]);
                    }
            };
            traverseTree(data);
            return data;
        };

        this.showDialog = function (content, title, type, callback, timeOut) {
            ///<summary>displays model dialog</summary>
            ///<param name="content" type="String">text/html</param>
            ///<param name="title" type="String">Title of the dialog</param>
            ///<param name="type" type="Number" optional="true">0 = OK , 1 = OK/Cancel(Optional)</param>
            ///<param name="callback" type="Function">function to execute after operation is performed</param>
            ///<param name="timeOut" type="Number" optional="true">No of Millisecond</param>

            function handleAction() {
                var id = event.target.id;
                id = id.substr(12, id.length - 12);
                $('#sysDialogBox').modal('hide'); //Close dialog
                //$('#sysDialogBox').remove(); //Remove html
                $('.modal-backdrop.show.fade').remove();//remove backdrop
                if (isCallback) //handle callback (if found implemented)
                    callback(id);
            };

            //Default handling
            type = type || 0;
            timeOut = timeOut || 0;
            var isCallback = typeof callback === 'function';

            //Remove existing dialog box (if found)
            if ($('#sysDialogBox').length)
                $('#sysDialogBox').remove();

            //Prepare HTML
            var html = '<div id="sysDialogBox" class="modal fade"><div class="modal-dialog" role="document"><div class="modal-content"><div class="modal-header">';
            html += '<h5 class="modal-title">' + title + '</h5>';
            html += '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>';
            html += '</div><div class="modal-body">';
            html += content;
            html += '</div><div class="modal-footer">';
            switch (type) {
                case 0: //OK
                    html += '<button id="sysDialogBoxOk" type="button" style="margin-right:220px;" class="k-button">OK</button>';
                    break;

                case 1: //Submit & Cancel
                    html += '<button id="sysDialogBoxSubmit" type="button" class="k-button">Submit</button>';
                    html += '<button id="sysDialogBoxCancel" type="button" class="k-button" style="margin-right:150px;">Cancel</button>';
                    break;
            }
            html += '</div></div></div></div>';

            //Insert HTML
            $(document.body).append(html);
            $('#sysDialogBox').modal('show');
            //Event binding
            switch (type) {
                case 0:
                    $('#sysDialogBoxOk').click(handleAction);
                    break;

                case 1:
                    $('#sysDialogBoxSubmit').click(handleAction);
                    $('#sysDialogBoxCancel').click(handleAction);
                    break;
            }
        };
    };
    window.helper = new _helper();
}());