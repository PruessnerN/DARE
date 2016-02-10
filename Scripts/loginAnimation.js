var working = false;
function submitLoginForm() {
    if (working) return;
    working = true;
    var $this = $('.login'),
      $state = $this.find('button > .state');
    $this.addClass('loading');
    $state.html('Authenticating');
    $('.login').submit();
    var msg = '@ViewBag.LoginMsg';
    if (msg == "Welcome back!") {
        setTimeout(function () {
            $this.addClass('ok');
            $state.html('Welcome back!');
        }, 3000);
    } else {
        setTimeout(function () {
            $state.html('Incorrect username or password!');
            $this.addClass('loginError');
            working = false;
        }, 4000);
    }
};