// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('click', (event) => {
  if (!(event.target instanceof Element)) {
    return;
  }

  const createAccountLink = event.target.closest('[data-create-account]');
  if (createAccountLink instanceof HTMLAnchorElement) {
    event.preventDefault();
    openAccountCreateModal(createAccountLink.href);
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

async function openAccountCreateModal(createUrl) {
  try {
    const response = await fetch(createUrl, { headers: { Accept: 'application/json' } });
    if (!response.ok) {
      throw new Error('Could not load the account form. Please try again.');
    }

    const currencies = await response.json();
    showAccountCreateModal(createUrl, currencies);
  } catch (error) {
    showAccountMessage(error instanceof Error ? error.message : 'Could not load the account form. Please try again.');
  }
}

function showAccountCreateModal(createUrl, currencies) {
  const template = document.querySelector('#account-create-modal-template');
  if (!(template instanceof HTMLTemplateElement)) {
    showAccountMessage('Could not load the account form. Please try again.');
    return;
  }

  const modalElement = document.createElement('div');
  modalElement.className = 'modal fade account-create-modal';
  modalElement.tabIndex = -1;
  modalElement.setAttribute('aria-labelledby', 'account-create-title');
  modalElement.setAttribute('aria-hidden', 'true');
  modalElement.append(template.content.cloneNode(true));

  const form = modalElement.querySelector('form');
  const currencySelect = modalElement.querySelector('#account-currency');
  form.action = createUrl;

  currencies.forEach((currency) => {
    const option = document.createElement('option');
    option.value = currency.id;
    option.textContent = `${currency.name} (${currency.code})`;
    currencySelect.append(option);
  });

  form.addEventListener('submit', async (event) => {
    event.preventDefault();
    clearAccountFormErrors(form);

    if (!form.reportValidity()) {
      return;
    }

    const submitButton = form.querySelector('[data-submit-account]');
    submitButton.disabled = true;

    try {
      const response = await fetch(createUrl, {
        method: 'POST',
        body: new FormData(form),
        headers: { Accept: 'application/json' }
      });
      const result = await response.json();

      if (!response.ok) {
        displayAccountFormErrors(form, result.errors || {});
        return;
      }

      window.location.assign(result.redirectUrl);
    } catch {
      const formError = form.querySelector('[data-form-error]');
      formError.textContent = 'Could not create the account. Please try again.';
      formError.classList.remove('d-none');
    } finally {
      submitButton.disabled = false;
    }
  });

  modalElement.addEventListener('hidden.bs.modal', () => modalElement.remove(), { once: true });
  document.body.append(modalElement);
  bootstrap.Modal.getOrCreateInstance(modalElement).show();
}

function clearAccountFormErrors(form) {
  form.querySelectorAll('.is-invalid').forEach((input) => input.classList.remove('is-invalid'));
  form.querySelectorAll('[data-validation-for]').forEach((message) => { message.textContent = ''; });

  const formError = form.querySelector('[data-form-error]');
  formError.textContent = '';
  formError.classList.add('d-none');
}

function displayAccountFormErrors(form, errors) {
  const formError = form.querySelector('[data-form-error]');
  const unassignedErrors = [];

  Object.entries(errors).forEach(([field, messages]) => {
    const input = form.elements.namedItem(field);
    const validationMessage = form.querySelector(`[data-validation-for="${field}"]`);
    const message = Array.isArray(messages) ? messages.join(' ') : String(messages);

    if (input instanceof HTMLElement && validationMessage) {
      input.classList.add('is-invalid');
      validationMessage.textContent = message;
    } else {
      unassignedErrors.push(message);
    }
  });

  if (unassignedErrors.length) {
    formError.textContent = unassignedErrors.join(' ');
    formError.classList.remove('d-none');
  }
}

function showAccountMessage(message) {
  const alert = document.createElement('div');
  alert.className = 'alert alert-danger account-create-alert';
  alert.setAttribute('role', 'alert');
  alert.textContent = message;
  document.querySelector('.account-page').prepend(alert);
}
