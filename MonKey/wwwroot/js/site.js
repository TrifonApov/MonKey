// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('click', (event) => {
  if (!(event.target instanceof Element)) {
    return;
  }

  const button = event.target.closest('[data-password-toggle]');
  if (!(button instanceof HTMLButtonElement)) {
    return;
  }

  const input = button.parentElement.querySelector('[data-password-toggle-input]');
  if (!(input instanceof HTMLInputElement)) {
    return;
  }

  const isVisible = input.type === 'password';
  input.type = isVisible ? 'text' : 'password';
  button.setAttribute('aria-label', isVisible ? 'Hide password' : 'Show password');
  button.setAttribute('aria-pressed', String(isVisible));
});
