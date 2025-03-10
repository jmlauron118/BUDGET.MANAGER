$(document).ready(function () {
    var enableTooltip = function () {
        $('.sidebar-menu, .sidebar-link, .sidebar-submenu').tooltip('enable');
    };

    var disableTooltip = function () {
        $('.sidebar-menu, .sidebar-link, .sidebar-submenu').tooltip('disable');
    };

    var openSidebar = function () {
        $('.sidebar-container').addClass('open');
        $('.sidebar-toggle').find('i.fa').removeClass('fa-bars');
        $('.sidebar-toggle').find('i.fa').addClass('fa-chevron-left');
        disableTooltip();
    };

    var collapseSidebar = function () {
        $('.sidebar-container').removeClass('open');
        $('.sidebar-toggle').find('i.fa').removeClass('fa-chevron-left');
        $('.sidebar-toggle').find('i.fa').addClass('fa-bars');
        setTimeout(function () {
            enableTooltip();
        }, 300);
    };

    enableTooltip();

    $('.sidebar-toggle').on('click', function () {
        if (!$('.sidebar-container').hasClass('open')) {
            openSidebar();
        } else {
            collapseSidebar();
        }
    });

    $('.sidebar-overlay').on('click', function () {
        collapseSidebar();
    });

    $('.sidebar-menu.sidebar-menu-dropdown').on('click', function () {
        $(this).toggleClass('active');
    });

    $('#userDropdown').on('show.bs.dropdown', function () {
        $(this).find('.dropdown-menu').first().stop(true, true).slideDown(300);
    });

    $('#userDropdown').on('hide.bs.dropdown', function () {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp(300);
    });

    //$('.dropdown').on('show.bs.dropdown', function () {
    //    $(this).find('.dropdown-menu').first().stop(true, true).slideDown(300);
    //});

    //$('.dropdown').on('hide.bs.dropdown', function () {
    //    $(this).find('.dropdown-menu').first().stop(true, true).slideUp(300);
    //});

    $('#txtUsername, #txtPassword').off('focus').on('focus', function () {
        $(this).closest('.form-group').toggleClass('focused');
    }).off('blur').on('blur', function () {
        $(this).closest('.form-group').toggleClass('focused');
    });

    $('.list-group .list-group-item').click(function () {
        var tab = $('.tab-content').find('.tab-pane.show');
        var tabId = $(this).attr('href');
        tab.removeClass('show');
        setTimeout(function () {
            $('.tab-content').find('.tab-pane').removeClass('active');
            $(tabId).addClass('active');
            setTimeout(function () {
                $(tabId).addClass('show');
            }, 100);
        }, 200);
    });

    $('.last-item').click(function () {
        $('.sb-content a').remove();
        $('.notif-item-content').remove();

        var id = GetCookie('UserRoleId');
        GetAllSentNotification(id);

        if (!$('.notif-container').hasClass('open')) {
            $('.notif-container').animate({ width: '300px' });
            $('.notif-container').addClass('open');
            $('#txtSearchNotif').val('');
            $($('.sb-content .dropdown-item')).css('display', '');
        }
    });

    $("body").mouseup(function (e) {
        if (!$('.notif-container').is(e.target) && $('.notif-container').has(e.target).length === 0) {
            $('.notif-container').animate({ width: '0px' });
            $('.notif-container').removeClass('open');
        }
    });
});