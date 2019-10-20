// This file is for setups like code snippets.
  $(function () {
    setTheme('index')

    // THEME CHECKBOX EVENT HANDLER:
    document.getElementById('theme-checkbox').addEventListener('change', (e) => {
      let that = e.target
      // Save the theme on localStorage (sessionStorage is not persistent scross sessions).
      localStorage.setItem('theme', that.checked)
      setTheme('index')
    })

    $('.tooltipped').tooltip({delay: 50})

    $('#color-picker').colorpicker({
      component: '.btn'
    });

    $('.datepicker-start, .datepicker-end').pickadate({
			selectMonths: true, // Creates a dropdown to control month
			selectYears: 15, // Creates a dropdown of 15 years to control year,
			today: 'Today',
			close: 'Ok',
			closeOnSelect: false, // Close upon selecting a date,
			format: 'dd-mm-yyyy'
		});

    updateCardsModal()
  });
