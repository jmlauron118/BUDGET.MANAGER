// @ts-nocheck
document.addEventListener("DOMContentLoaded", function () {
    Validations();

    document.getElementById("btnLogout").addEventListener("click", throttle(() => {
        $.post("../Login/Logout", function () {
            window.location = "../Login/Index";
            setTimeout(() => {
                Notif("You have been logged out successfully.", "success");
            });
        });
    }, 1000));
});

function Validations() {
    $(".number-only").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $(".pascal-input").on("keyup", function () {
        let value = this.value;
        let capitalized = value.replace(/\b\w/g, function (match) {
            return match.toUpperCase();
        });

        $(this).val(capitalized);
    });
}

function throttle(fn, limit) {
    let inThrottle;
    return function (...args) {
        if (!inThrottle) {
            fn.apply(this, args);
            inThrottle = true;
            setTimeout(() => (inThrottle = false), limit);
        }
    };
}

/**
 * Capitalize the first letter of each word in a string
 * @param {any} str  - string to capitalize
 * @returns - Capitalized string
 */
function CapitalizeWords(str) {
    return str.replace(/\b\w/g, function (char) {
        return char.toUpperCase();
    });
}

/**
 * Customized Snackbar notification
 * @param {string} notifText - Text to display in the notification. 
 * @param {string} notifType - Notification style. Options: success, warning, danger, default
 * @param {string} pos - Position of the notification popup. Options: topRight, topLeft, topCenter, bottomRight, bottmLeft, bottomCenter
 */
function Notif(
    notifText = "Nofitication message",
    notifType = "default",
    pos
) {
    const Color = {
        success: "badge-success",
        warning: "badge-warning",
        danger: "badge-danger",
        default: "badge-default"
    };

    const Position = {
        topRight: "top-right",
        topLeft: "top-left",
        topCenter: "top-center",
        bottomLeft: "bottom-left",
        bottomRight: "bottom-right",
        bottomCenter: "bottom-center",
    };

    // @ts-ignore
    Snackbar.show({
        text: notifText,
        customClass: Color[notifType] == undefined ? Color["default"] : Color[notifType],
        pos: Position[pos] == undefined ? Position["topRight"] : Position[pos],
        actionText: '<i class="fas fa-times"></i>',
        actionTextColor: "#ffffff"
    });
}

