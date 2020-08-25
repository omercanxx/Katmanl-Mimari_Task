function Send(task_id) {
    bootbox.confirm("Are you sure want to send?", function (result) {
        if (result) {
            $.ajax({
                url: '/Task/Send',
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
function Approve(task_id) {
    bootbox.confirm("Are you sure want to approve?", function (result) {
        if (result) {
            $.ajax({
                url: '/Task/Approve',
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

function Reject(task_id) {
    bootbox.prompt("What is your reason for reject", function (result) {
        if (result) {
            $.ajax({
                url: '/Task/Reject',
                data: { id: task_id, exp: result },
                type: 'POST',
                success: function (cevap) {
                    bootbox.alert("It was rejected.");
                    location.reload();
                }
            })
        }
    });
}


function Recovery(proje_id) {
    bootbox.confirm("Are you sure want to recovery?", function (result) {
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
function Delete(proje_id) {
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

function Show(task_id) {
    $.ajax({
        url: '/Task/Show',
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