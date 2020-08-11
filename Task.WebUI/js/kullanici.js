function gonder(task_id) {
    bootbox.confirm("Are you sure want to send?", function (result) {
        if (result) {
            $.ajax({
                url: '/Task/gonder',
                data: { id: task_id },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was sent to the manager.");
                    location.reload();
                }
            })
        }
    });
}
function onayla(task_id) {
    bootbox.confirm("Are you sure want to approve?", function (result) {
        if (result) {
            $.ajax({
                url: '/Task/onayla',
                data: { id: task_id },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was sent to the manager.");
                    location.reload();
                }
            })
        }
    });
}

function reddet(task_id) {
    bootbox.prompt("What is your reason for decline", function (result) {
        if (result) {
            $.ajax({
                url: '/Task/reddet',
                data: { id: task_id, exp: result },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was declined.");
                    location.reload();
                }
            })
        }
    });
}


function kurtar(proje_id) {
    bootbox.confirm("Are you sure want to recover?", function (result) {
        if (result) {
            $.ajax({
                url: '/Admin/Recovery',
                data: { id: proje_id },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was recovered.");
                    location.reload();
                }
            })
        }
    });
}
function sil(proje_id) {
    bootbox.confirm("Are you sure want to delete?", function (result) {
        if (result) {
            $.ajax({
                url: '/Project/Delete',
                data: { id: proje_id },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was deleted.");
                    location.reload();
                }
            })
        }
    });
}

function goster(task_id) {
    $.ajax({
        url: '/Task/goster',
        data: { id: task_id },
        type: 'POST',
        success: function (cevap) {
            bootbox.alert({
                title: "Reason for reject",
                message: cevap
            });
        }
    })
}
