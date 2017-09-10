
// JavaScript

/*
if (!value)
    null
    undefined
    NaN
    ""
    0
    false
*/

function zAlert(message) {
    if (message) {
        message = "<h4>" + message + "</h4>";
    }

    $("#ZAlert").html(message);
    if (message) {
        $("#ZAlert").ejDialog({
            height: 270,
            maxHeight: 540,
            minHeight: 270,
            width: 960,
            maxWidth: 1140,
            minWidth: 540
        });
        $("#ZAlert").ejDialog("open");
    }
}

function zContains(array, value) {
    return array.indexOf(value) >= 0;
}

function zFormat(string, args) {
    // format("{0} or {1}", ["Black", "White"])); 
    $.each(args, function (i, n) {
        string = string.replace(new RegExp("\\{" + i + "\\}", "g"), n);
    });

    return string;
}

function zOne(value) {
    return !value || value < 1 ? 1 : value;
}

function zParseFloat(value) {
    var result;

    try {
        var result = parseFloat(value);
        if (!result || !(typeof result === "number")) {
            result = null;
        }
    }
    catch (exception) {
        result = null;
    }

    return result;
}

function zParseInt(value) {
    var result;

    try {
        var result = parseInt(value);
        if (!result || !(typeof result === "number")) {
            result = null;
        }
    }
    catch (exception) {
        result = null;
    }

    return result;
}

function zParseJSON(json) {
    var result;

    try {
        var result = JSON.parse(json);
        if (!result || !(typeof result === "object")) {
            result = null;
        }
    }
    catch (exception) {
        result = null;
    }

    return result;
}

function zReplaceAll(string, find, replace) {
    return string.replace(new RegExp(find, 'g'), replace);
}

function zRound(value, digits) {
    return Number(Math.round(value + 'e' + digits) + 'e-' + digits);
}

function zZero(value) {
    return !value || value < 0 ? 0 : value;
}

function zNotZero(value) {
    return !value ? 0 : value;
}

// AJAX

// jqXHR = jQuery XMLHttpRequest

/*
done data
{
    "Response":
    {
        "Preco":0
    }
}

done jqXHR
{
    "readyState":4,
    "responseText":
    {
        "Response":
        {
            "Preco":0
        }
    },
    "responseJSON":
    {
        "Response":
        {
            "Preco":0
        }
    },
    "status":200,
    "statusText":"OK"
}

fail jqXHR
{
    "readyState":4,
    "responseText":
    {
        "OperationResult":
        {
            "Data":null,"ErrorCode":"","ErrorMessage":"","Html":"\\u003clabel class=\\"label label-danger\\"\\u003eErro: ERRO\\u003c/label\\u003e\\u003cbr /\\u003e","Ok":false,"StatusCode":"","StatusMessage":"","OperationErrors":[{"ErrorCode":"","ErrorMessage":"Siegmar I. J. Gieseler","ErrorMembers":[]}],"Text":"Erro: Siegmar I. J. Gieseler\\n"}}","responseJSON":{"OperationResult":{"Data":null,"ErrorCode":"","ErrorMessage":"","Html":"Erro: Siegmar I. J. Gieseler","Ok":false,"StatusCode":"","StatusMessage":"","OperationErrors":[{"ErrorCode":"","ErrorMessage":"ERRO","ErrorMembers":[]}],"Text":"Erro: ERRO\n"
        }
    },
    "status":400,
    "statusText":"Bad Request"
}
*/

function zAjaxError(jqXHR) {
    var error = "";

    if (jqXHR) {
        if (jqXHR.responseJSON) {
            if (jqXHR.responseJSON.OperationResult) {
                error = jqXHR.responseJSON.OperationResult.Html;
            } else {
                error = jqXHR.responseJSON;
            }
        } else if (jqXHR.responseText) {
            var response = zParseJSON(jqXHR.responseText);
            if (response && response.OperationResult) {
                error = response.OperationResult.Html;
            } else {
                error = jqXHR.responseText;
            }
        } else {
            error = JSON.stringify(jqXHR);
        }
    }

    return error;
}

function zAjaxFailure(jqXHR, textStatus, errorThrown) {
    try {
        if (jqXHR && jqXHR.responseJSON && jqXHR.responseJSON.Url) {
            window.location.replace(jqXHR.responseJSON.Url);
        }

        zAlert(zAjaxError(jqXHR));
    } catch (exception) {
        zExceptionMessage("", "zAjaxFailure", exception.message);
    }
}

function zAjaxLoadAsync(id, ajaxUrl) {
    $("#" + id).load(ajaxUrl);
}

function zAjaxLoadSync(id, ajaxUrl) {
    try {
        jQuery.ajaxSetup({ async: false });
        $("#" + id).load(ajaxUrl);
    } finally {
        jQuery.ajaxSetup({ async: true });
    }
}

function zAjaxLoadComplete(responseText, textStatus, jqXHR) {
    try {
        var response = zParseJSON(jqXHR.responseText);
        if (response && response.Url) {
            window.location.replace(response.Url);
        }

        if (textStatus === "error") {
            zAlert(zAjaxError(jqXHR));
        }
    } catch (exception) {
        zExceptionMessage("", "zAjaxLoadComplete", exception.message);
    }
}

function zAjaxSuccess(data, textStatus, jqXHR) {
    try {
        if (data) {
            if (data.OperationResult) {
                zShowOperationResult(data.OperationResult);
            }

            var url = data.Url;
            if (!url) {
                var controller = data.Controller;
                if (controller) {
                    url = zUrlDictionaryRead(controller);
                }
            }
            if (url) {
                $("#ZAjax").load(url, function (responseText, textStatus, jqXHR) {
                    zAjaxLoadComplete(responseText, textStatus, jqXHR);
                });
            }
        }
    } catch (exception) {
        zExceptionMessage("", "zAjaxSuccess", exception.message);
    }
}


// EDM

function zEDMCssClass(fileAcronym) {
    var cssClass = "";

    switch (fileAcronym) {
        case "mp3":
            cssClass = "z-fileAudio";
            break;
        case "xls":
        case "xlsx":
            cssClass = "z-fileExcel";
            break;
        case "jpg":
        case "png":
            cssClass = "z-fileImage";
            break;
        case "pdf":
            cssClass = "z-filePDF";
            break;
        case "avi":
        case "mov":
        case "mp4":
        case "mpeg":
        case "wmv":
            cssClass = "z-fileVideo";
            break;
        case "doc":
        case "docx":
            cssClass = "z-fileWord";
            break;
        default:
            cssClass = "z-fileUnknown";
            break;
    }

    return cssClass;
}


// Errors & Exceptions

function zExceptionMessage(fileName, functionName, exception) {
    fileName = fileName ? fileName : "";
    functionName = functionName ? functionName : "";
    var message = exception && exception.message ? exception.message : "";
    var stack = exception && exception.stack ? exception.stack : "";

    return "<b>File:</b> " + fileName +
        "<br/><b>Function:</b> " + functionName+
        "<br/><b>Exception:</b> " + message +
        "<br/><button data-toggle='collapse' data-target='#zStack'>...</button>" +
        "<div id='zStack' class='collapse'>" + zReplaceAll(stack, "  at ", "<br/>at ") + "</div>";
}

function zLookupElements(data, elements, culture) {
    // ZLibrary.Mvc.LookupModelElement
    // elements.Id
    // elements.Property
    if (elements) {
        var elementsLength = elements.length;
        for (var i = 0, length = elementsLength; i < length; i++) {
            var element = elements[i];
            // $("#Id")
            if ($("#" + element.Id).length) {
                // data.Property
                if (data.hasOwnProperty(element.Property)) {
                    var value = Globalize.format(data[element.Property]);
                    try {
                        data[element.Property].getFullYear(); // Date
                        value = Globalize.format(data[element.Property], "d");
                    } catch (exception) { }
                    $("#" + element.Id).val(value);

                    //$("#" + element.Id).val(data[element.Property].toLocaleString(culture));
                }
            }
        }
    }
}

function zShowOperationResult(operationResult) {
    //if (operationResult.Html) {
    //    $("#ZOperationResult").html(operationResult.Html);
    //}

    if (operationResult.Html) {
        zAlert(operationResult.Html);
    }
}


// Local Storage

function zLocalStorageClear() {
    window.localStorage.clear();
}

function zLocalStorageGet(item) {
    return window.localStorage.getItem(item);
}

function zLocalStorageRemove(item) {
    return window.localStorage.removeItem(item);
}

function zLocalStorageSet(item, data) {
    window.localStorage.setItem(item, data);
}


// On*View

function zOnCollectionView(model, dataProfile, dataSourceUrl) {
    var ejGrid = zGrid("Grid_" + dataProfile.Class.Name);

    // Master-Detail Filtering & Search
    if (model.IsMasterDetail) {
        // Filtering
        if (ejGrid.model.filterSettings.filteredColumns.length > 0) {
            ejGrid.clearFiltering();
        }
        // Search
        if (ejGrid.model.searchSettings.key) {
            ejGrid.clearSearching();
        }
        // Current Page
        if (ejGrid.model.pageSettings.currentPage !== 1) {
            ejGrid.model.pageSettings.currentPage = 1;
        }
    }
    else {
        var search = zSearchDictionaryRead(dataProfile.Class.Name);
        if (search) {
            ejGrid.model.searchSettings.key = search;
        }    
    }
    ejGrid.model.searchSettings.fields = dataProfile.GridSearchProperties;

    if (!model.IsMasterDetail) {
        ejGrid.setModel({
            allowGrouping: true,
            toolbarSettings: {
                toolbarItems: [
                    ej.Grid.ToolBarItems.Search,
                    ej.Grid.ToolBarItems.Add,
                    ej.Grid.ToolBarItems.Edit,
                    ej.Grid.ToolBarItems.Delete
                    //ej.Grid.ToolBarItems.ExcelExport,
                    //ej.Grid.ToolBarItems.PdfExport,
                    //ej.Grid.ToolBarItems.WordExport
                ]
            }
        });
    } else {
        ejGrid.setModel({
            allowGrouping: false,
            toolbarSettings: {
                toolbarItems: [
                    //ej.Grid.ToolBarItems.Search,
                    ej.Grid.ToolBarItems.Add,
                    ej.Grid.ToolBarItems.Edit,
                    ej.Grid.ToolBarItems.Delete
                ]
            }
        });
    }

    // Tooltip
    $("#Grid_" + dataProfile.Class.Name + "_Grid_" + dataProfile.Class.Name + "_Toolbar").removeAttr("data-content");

    // DataSource
    if (!model.IsMasterDetail) {
        zGridDataSource("Grid_" + dataProfile.Class.Name, dataSourceUrl);
    }

    // <div></div>
    // Search.cshtml                <div style = "display: none;">
    // _EntityCollection.cshtml         <div id="Collection_Entity">
    $("#Collection_" + dataProfile.Class.Name).parent().show();

    // Activity
    //if (model.ActivityOperations.IsSearch) {
    //    $("#Collection_" + dataProfile.Class.Name).css("display", "block");
    //}
    //else {
    //    $("#Collection_" + dataProfile.Class.Name).css("display", "none");
    //}

    zShowOperationResult(model.OperationResult);
}

function zOnCRUDView(model) {
    var controllerAction = model.ControllerAction ? model.ControllerAction.toLowerCase() : "";
    var isMasterDetail = model.IsMasterDetail ? model.IsMasterDetail : false;
    var customButtonSave = model.CustomButtonSave ? model.CustomButtonSave : false;
    var customButtonOK = model.CustomButtonOK ? model.CustomButtonOK : false;

    $("#Button_Save").hide();
    $("#Button_OK").hide();

    if (controllerAction === "create" || controllerAction === "update" || customButtonSave) {
        $("#Button_Save").show();
    }

    if (isMasterDetail) {
        $("#Button_Save").hide();
    }

    if (controllerAction === "create" || controllerAction === "update" || controllerAction === "delete" || customButtonOK) {
        $("#Button_OK").show();
    }
}

function zOnItemView(model, dataProfile) {
    var controllerAction = model.ControllerAction ? model.ControllerAction.toLowerCase() : "";

    // Read-Only
    var readOnly = controllerAction === "delete" || controllerAction === "read" || model.IsReadOnly;
    $("input.form-control").not(":input[type=button], :input[type=image], :input[type=reset], :input[type=submit]").prop("readonly", readOnly);
    // ReadOnly.js
    // https://github.com/haggen/readonly
    readonly('select', readOnly);
    $("textarea").prop("readonly", readOnly);
    $("input[name*='_Lookup'][type!='checkbox']").prop("readonly", true);
    if (readOnly) {
        $("input[type='checkbox']").prop("disabled", true);

        $("input[data-role='ejdatepicker']").each(function() {
            $(this).data("ejDatePicker").disable();
        });
        $("input[data-role='ejdatetimepicker']").each(function() {
            $(this).data("ejDateTimePicker").disable();
        });

        $("img.z-buttonLookup").hide();
    }
    else {
        $("img.z-buttonLookup").show();
    }

    var i;

    // Hidden
    var hiddenProperties = dataProfile.EditHiddenProperties;
    var hiddenPropertiesLength = hiddenProperties.length;
    for (i = 0; i < hiddenPropertiesLength; i++) {
        $("#Group_" + dataProfile.Class.Name + "_" + hiddenProperties[i]).hide();
    }

    // Read-Only
    var readOnlyProperties = dataProfile.EditReadOnlyProperties;
    var readOnlyPropertiesLength = readOnlyProperties.length;
    for (i = 0; i < readOnlyPropertiesLength; i++) {
        $("#" + dataProfile.Class.Name + "_" + readOnlyProperties[i]).prop("readonly", true);

        $("#" + dataProfile.Class.Name + "_" + readOnlyProperties[i] + "_LookupButton").hide();

        $("input[type='checkbox'][id='" + dataProfile.Class.Name + "_" + readOnlyProperties[i] + "']").removeProp("readonly");
        $("input[type='checkbox'][id='" + dataProfile.Class.Name + "_" + readOnlyProperties[i] + "']").prop("disabled", true);

        $("input[data-role='ejdatepicker'][id='" + dataProfile.Class.Name + "_" + readOnlyProperties[i] + "']").each(function () {
            $(this).data("ejDatePicker").disable();
        });
        $("input[data-role='ejdatetimepicker'][id='" + dataProfile.Class.Name + "_" + readOnlyProperties[i] + "']").each(function () {
            $(this).data("ejDateTimePicker").disable();
        });
    }

    var id;

    // Collections (PK)
    var hiddenCollections = dataProfile.EditHiddenCollections;
    var hiddenCollectionsLength = hiddenCollections.length;
    for (i = 0; i < hiddenCollectionsLength; i++) {
        id = "TabSheet_" + dataProfile.Class.Name + "_" + hiddenCollections[i];
        $("a[href='#" + id + "']").css("display", "none");
        $("#" + id).css("display", "none");
    }
    var collections = dataProfile.EditCollections;
    var collectionsLength = collections.length;
    for (i = 0; i < collectionsLength; i++) {
        id = "TabSheet_" + dataProfile.Class.Name + "_" + collections[i];
        if (controllerAction === "create") {
            $("a[href='#" + id + "']").css("display", "none");
            $("#" + id).css("display", "none");
        } else {
            $("a[href='#" + id + "']").removeAttr("display");
            $("#" + id).removeAttr("display");
            //$("a[href='#" + id + "']").css("display", "block");
            //$("#" + id).css("display", "block");
        }
    }
    if (controllerAction == "create") {
        zTabDictionaryWrite(dataProfile.Class.Name, 0);
    }

    // ENTER => TAB
    $("input").keydown(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key === 13) {
            e.preventDefault();
            var inputs = $(this).closest("form").find(":input:visible");
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    // Tab
    var tabIndex = zTabDictionaryRead(dataProfile.Class.Name);
    if (tabIndex && tabIndex >= 0) {
        var ejTab = zTab("Tab_" + dataProfile.Class.Name);
        ejTab.setModel({
            selectedItemIndex: tabIndex
        });
    }

    // <div></div>
    // CRUD.cshtml                  <div id="Form_Entity" style = "display: none;">
    // CRUD.cshtml                      <div>
    // _EntityItem.cshtml                   <div id="Item_Entity">
    $("#Item_" + dataProfile.Class.Name).parent().parent().show();

    zShowOperationResult(model.OperationResult);
}

function zOnLookupView(model, dataProfile, ejGrid) {
    // Search
    ejGrid.model.searchSettings.fields = dataProfile.GridSearchProperties;

    zShowOperationResult(model.OperationResult);
}


// Session Storage

function zSessionStorageClear() {
    window.sessionStorage.clear();
}

function zSessionStorageGet(item) {
    return window.sessionStorage.getItem(item);
}

function zSessionStorageRemove(item) {
    window.sessionStorage.removeItem(item);
}

function zSessionStorageSet(item, data) {
    window.sessionStorage.setItem(item, data);
}


// Syncfusion

function zGrid(gridId) {
    var ejGrid = $("#" + gridId).data("ejGrid");
    if (!ejGrid) {
        ej.widget.init($("#" + gridId).parent()); // #Grid_Entity => #Collection_Entity
        ejGrid = $("#" + gridId).data("ejGrid");
    }

    return ejGrid;
}

function zGridDataSource(gridId, dataSourceUrl, isRefresh) {
    if ($("#" + gridId).length) {
        if (!isRefresh) {
            isRefresh = false;
        }

        var ejGrid = zGrid(gridId);

        if (!ejGrid.model.dataSource) {
            //ejGrid.setModel({
            //    dataSource: ej.DataManager({
            //        adaptor: new ej.UrlAdaptor(),
            //        url: dataSourceUrl
            //    })
            //});

            ejGrid.dataSource(ej.DataManager({
                adaptor: new ej.UrlAdaptor(),
                url: dataSourceUrl
            }));
        }

        if (isRefresh) {
            ejGrid.refreshContent();
        }
    }
}

function zTab(tabId) {
    var ejTab = $("#" + tabId).data("ejTab");
    if (!ejTab) {
        ej.widget.init($("#" + tabId).parent()); // #Tab_Entity => #Item_Entity
        ejTab = $("#" + tabId).data("ejTab");
    }

    return ejTab;
}
