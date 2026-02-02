import { EmployeeViewModel } from './viewmodels/EmployeeViewModel.js';
import { ConfigService } from './services/ConfigService.js';

document.addEventListener('DOMContentLoaded', async () => {

    await ConfigService.loadConfig();
    const employeeVM = new EmployeeViewModel();
    employeeVM.refresh();
    const btnAdd = document.getElementById('btn-add');
    btnAdd?.addEventListener('click', () => employeeVM.openModal());
});