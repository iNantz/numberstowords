$(document).ready(function () {
    $("#txtInput").inputmask();

    // hide the answer
    $("#pnlAnswer").hide();

    // if button convert or currency check is click call numbers to words
    $("#btnConvert,#chkCurrency").click(function () {
        numbersToWords();
    });

    // if input changes call numbers to words or hide the answer panel if empty
    $("#txtInput").keyup(function () {
        if ($.trim($("#divAnswer").text).length == 0) {
            $("#divAnswer").text("");
            $("#pnlAnswer").hide("slow");
        }
        else
            numbersToWords();
    });

    // call web api if answer is not yet in cache otherwise get it from cache
    function numbersToWords() {
        var key = $("#txtInput").val() + $("#chkCurrency").is(":checked");
        var webapiurl = $("#txtInput").data("weburl") + "/" + $("#txtInput").val().replace(".", "_") + "/" + $("#chkCurrency").is(":checked");

        $.ajax({
            type: "GET",
            url: webapiurl,
            cache: true,
            beforeSend: function () {
                var cacheData = getCacheData(key);

                if (cacheData != null) {
                    showAnswer(cacheData);
                    return false;
                }

                return true;
            },
            success: function (returnData) {
                showAnswer(returnData);
                setCacheData(key, returnData);
            },
            error: function (request, status, error) {
                console.log(status);
                $("#pnlAnswer").hide("slow");
            }
        });
    }

    // show answer
    function showAnswer(data)
    {
        $("#divAnswer").text(data);
        $("#pnlAnswer").show("slow");
    }

    // get the cache data
    function getCacheData(key, islocal) {
        return islocal ? getLocalData(key) : getSessionData(key);
    }

    // save data to chache
    function setCacheData(key, data, islocal) {
        return islocal ? setLocalData(key, data) : setSessionData(key, data);
    }

    // get json data from session storage
    function getSessionData(key) {
        var retval = null;
        if (Modernizr.sessionstorage) {
            retval = sessionStorage.getItem(key);
            if (retval != null && retval.length > 0) {
                retval = JSON.parse(retval);
            }
        }

        return retval;
    }

    // save json data to session storage
    function setSessionData(key, data) {
        if (Modernizr.sessionstorage) {
            sessionStorage.setItem(key, JSON.stringify(data));
        }
    }

    // get json data from local storage
    function getLocalData(key) {
        var retval = null;
        if (Modernizr.localstorage) {
            retval = localStorage.getItem(key);
            if (retval != null && retval.length > 0) {
                retval = JSON.parse(retval);
            }
        }

        return retval;
    }

    // save json data to local storage
    function setLocalData(key, data) {
        if (Modernizr.localstorage) {
            localStorage.setItem(key, JSON.stringify(data));
        }
    }
});


