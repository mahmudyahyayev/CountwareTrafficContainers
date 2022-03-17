var waiting = '<i title="waiting fire time" style="color:gray" class="fa fa-hourglass-start">';
var running = '<i class="text-success fa fa-play"></i>';
var paused = '<i class="text-warning fa fa-pause"></i>';

$(document).ready(function () {
    getScheduledJobs();
});

function getScheduledJobs() {

    $('#tblLiveJobs').empty();

    $.get("/jobs", function () {})
        .done(function (jobs) {

            if (jobs == null)
                return;

            $.each(jobs, function (i, job) {

                var dataStatus = waiting;

                if (job.triggerState == 'Paused')
                    dataStatus = paused;
                else if (job.triggerState == 'Blocked' || job.triggerState == 'Complete')
                    dataStatus = running;

                var $tr = $('<tr>').attr('id', job.jobName).append(
                    $('<td>').text(job.jobName),
                    $('<td>').text(job.description),
                    $('<td>').text(job.triggerDescription),
                    $('<td data-previoustime>').text(job.previousFireTime),
                    $('<td data-nexttime>').text(job.nextFireTime),
                    $('<td data-firetime>').text(job.fireTime),
                    $('<td data-runtime>').text(''),
                    $('<td data-status>').html(dataStatus).addClass('text-center'),
                    $('<td>').html('<btn onclick="triggerJob(this)" class="btn btn-sm btn-outline-success btn-round btn-icon p-1"><i class="fa fa-play"></i></btn>')
                        .append('<btn onclick="pauseJob(this)" class="btn btn-sm btn-outline-warning btn-round btn-icon"><i class="fa fa-pause"></i></btn>')
                ).appendTo('#tblLiveJobs');
            });

        })
        .fail(function () {
            alert("Error while getting jobs");
        })
}

connection.on("jobExecuting", (job) => {

    $('#' + job.jobName).find('[data-nexttime]').text(job.nextFireTime);
    $('#' + job.jobName).find('[data-firetime]').text(job.fireTime);
    $('#' + job.jobName).find('[data-previoustime]').text(job.previousFireTime);
    $('#' + job.jobName).find('[data-status]').html(running);
});

connection.on("triggerMisfired", (message) => {
    $.notify({
        icon: "nc-icon nc-alert-circle-i",
        message: message
    },
        {
            type: 'danger',
            timer: 3000,
        }
    )
});

connection.on("jobExecuted", (job) => {

    $('#' + job.jobName).find('[data-runtime]').text(job.lastRunTime);

    var dataStatus = waiting;

    if (job.triggerState == 'Paused')
        dataStatus = paused;

    $('#' + job.jobName).find('[data-status]').html(dataStatus);
});


connection.on("jobPaused", (jobName) => {
    $('#' + jobName).find('[data-status]').html(paused);
    $.notify({
        icon: "nc-icon nc-alert-circle-i",
        message: jobName + ' paused!'
    },
        {
            type: 'success',
            timer: 3000,
        }
    )
});

connection.on("jobResumed", (jobName) => {
    $('#' + jobName).find('[data-status]').html(waiting);
    $.notify({
        icon: "nc-icon nc-alert-circle-i",
        message: jobName + ' resumed!'
    },
        {
            type: 'success',
            timer: 3000,
        }
    )
});


connection.on("jobDeleted", (jobName) => {

    $('#' + jobName).remove();

    $.notify({
        icon: "nc-icon nc-alert-circle-i",
        message: jobName + ' deleted!'
    },
        {
            type: 'success',
            timer: 3000,
        }
    )
});

connection.on("jobsPaused", (message) => {
    $('tr').find('[data-status]').html(paused);
    $.notify({
        icon: "nc-icon nc-alert-circle-i",
        message: message
    },
        {
            type: 'success',
            timer: 3000,
        }
    )
});

connection.on("jobsResumed", (message) => {
    $('tr').find('[data-status]').html(waiting);
    $.notify({
        icon: "nc-icon nc-alert-circle-i",
        message: message
    },
        {
            type: 'success',
            timer: 3000,
        }
    )
});

function triggerJob(el) {

    var jobName = $(el).parent().parent().attr('id');

    $.ajax({
        url: '/jobs/' + jobName,
        type: 'POST',
        success: function (result) {
        },
        fail: function (result) {
            $.notify({
                icon: "nc-icon nc-alert-circle-i",
                message: 'Job trigger failed!'
            },
                {
                    type: 'danger',
                    timer: 4000,
                }
            )
        }
    });
}

function pauseJob(el) {

    var jobName = $(el).parent().parent().attr('id');

    $.ajax({
        url: '/jobs/' + jobName,
        type: 'DELETE',
        success: function (result) {
            $('#' + jobName).find('[data-status]').html(paused);
        },
        fail: function (result) {
            $.notify({
                icon: "nc-icon nc-alert-circle-i",
                message: 'failed!'
            },
                {
                    type: 'danger',
                    timer: 4000,
                }
            )
        }
    });
}

function pauseAll() {

    $.ajax({
        url: '/jobs',
        type: 'DELETE',
        success: function (result) {
        },
        fail: function (result) {
            $.notify({
                icon: "nc-icon nc-alert-circle-i",
                message: 'failed to pause all!'
            },
                {
                    type: 'danger',
                    timer: 4000,
                }
            )
        }
    });
}

function resumeAll() {

    $.ajax({
        url: '/jobs',
        type: 'PUT',
        success: function (result) {
        },
        fail: function (result) {
            $.notify({
                icon: "nc-icon nc-alert-circle-i",
                message: 'failed to resume all!'
            },
                {
                    type: 'danger',
                    timer: 4000,
                }
            )
        }
    });
}


$('#btnRefresh').click(function () {
    refresh();
});

function refresh() {
    getScheduledJobs();
}