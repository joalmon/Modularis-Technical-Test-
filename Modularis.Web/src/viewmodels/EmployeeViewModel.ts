import { Employee } from '../models/Employee.js';
import { EmployeeService } from '../services/EmployeeService.js';

export class EmployeeViewModel {
    private employees: Employee[] = [];
    private modal: HTMLElement | null;
    private form: HTMLFormElement | null;
    private tableBody: HTMLElement | null;

    constructor() {
        this.modal = document.getElementById('modal-employee');
        this.form = document.getElementById('employee-form') as HTMLFormElement;
        this.tableBody = document.getElementById('employee-list-body');

        this.setupEventListeners();

        if (this.tableBody) {
            this.fetchAndRender(this.tableBody);
        }
    }

    private setupEventListeners() {
        this.form?.addEventListener('submit', async (e) => {
            e.preventDefault();
            await this.save();
        });

        document.getElementById('btn-cancel')?.addEventListener('click', () => this.closeModal());
    }

    private generateGUID(): string {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c) => {
            const r = Math.random() * 16 | 0;
            const v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    openModal() {
        const errorBanner = document.getElementById('form-error-banner');
        if (errorBanner) errorBanner.style.display = 'none';

        if (this.modal) {
            this.form?.reset();
            const statusGroup = document.getElementById('status-group');
            if (statusGroup) statusGroup.style.display = 'none';

            this.modal.style.display = 'flex';
        }
    }

    closeModal() {
        if (this.modal) this.modal.style.display = 'none';
    }

    openEditModal(id: string) {
        const employee = this.employees.find(e => e.PersonID === id);
        if (!employee) return;

        const idInput = document.getElementById('emp-id') as HTMLInputElement;
        const firstNameInput = document.getElementById('emp-firstname') as HTMLInputElement;
        const lastNameInput = document.getElementById('emp-lastname') as HTMLInputElement;
        const ssnInput = document.getElementById('emp-ssn') as HTMLInputElement;
        const empNoInput = document.getElementById('emp-no') as HTMLInputElement;
        const statusSelect = document.getElementById('emp-status') as HTMLSelectElement;

        if (idInput) idInput.value = employee.PersonID;
        if (firstNameInput) firstNameInput.value = employee.FirstName;
        if (lastNameInput) lastNameInput.value = employee.LastName;
        if (ssnInput) ssnInput.value = employee.SSN;
        if (empNoInput) empNoInput.value = employee.EmployeeNo || '';
        if (statusSelect) {
            statusSelect.value = employee.Status.toString();
        }

        const statusGroup = document.getElementById('status-group');
        if (statusGroup) statusGroup.style.display = 'block';

        const modalTitle = document.getElementById('modal-title');
        if (modalTitle) modalTitle.innerText = "Edit Employee";

        if (this.modal) {
            this.modal.style.display = 'flex';
        }
    }

    private showErrorModal(message: string) {
        const modal = document.getElementById('error-modal');
        const msgPara = document.getElementById('error-modal-message');
        const closeBtn = document.getElementById('close-error-modal');

        if (modal && msgPara && closeBtn) {
            msgPara.innerText = message;
            modal.style.display = 'flex';
            closeBtn.onclick = () => modal.style.display = 'none';
        }
    }

    async save() {
        const form = document.getElementById('employee-form') as HTMLFormElement;
        const errorBanner = document.getElementById('form-error-banner');

        if (!form.checkValidity()) {
            if (errorBanner) errorBanner.style.display = 'block';
            return; 
        }

        if (errorBanner) errorBanner.style.display = 'none';

        try {
            const idInput = document.getElementById('emp-id') as HTMLInputElement;
            const isEditing = idInput.value !== "";

            const statusSelect = document.getElementById('emp-status') as HTMLSelectElement;
            const currentStatus = statusSelect ? parseInt(statusSelect.value) : 0;

            const employeeData: any = {
                PersonID: isEditing ? idInput.value : this.generateGUID(),
                FirstName: (document.getElementById('emp-firstname') as HTMLInputElement).value,
                LastName: (document.getElementById('emp-lastname') as HTMLInputElement).value,
                SSN: (document.getElementById('emp-ssn') as HTMLInputElement).value,
                EmployeeNo: (document.getElementById('emp-no') as HTMLInputElement).value,
                Status: currentStatus 
            };


            if (isEditing) {
                const original = this.employees.find(e => e.PersonID === employeeData.PersonID);

                const updatedEmployee = { ...original, ...employeeData };

                await EmployeeService.update(updatedEmployee);
            } else {
                employeeData.EmploymentStartDate = new Date().toISOString();
                employeeData.Status = 0;
                await EmployeeService.create(employeeData);
            }

            this.closeModal();
            await this.refresh(); 
        } catch (error) {

            console.error("[EmployeeViewModel][save] failed:", error);

            if (errorBanner) {
                errorBanner.innerHTML = `<strong>System Error:</strong><br>Unable to save employee. Please try again later.`;
                errorBanner.style.display = 'block';
            }
        }
    }

    async refresh() {
        if (this.tableBody) {
            await this.fetchAndRender(this.tableBody);
        }
    }

    async fetchAndRender(container: HTMLElement) {
        container.innerHTML = '<tr><td colspan="5">Loading...</td></tr>';
        try {
            this.employees = await EmployeeService.getAll();
            this.render(container);
        } catch (error) {
            console.error("[EmployeeViewModel][fetchAndRender] failed:", error);
            this.showErrorModal("The server is not responding. Unable to retrieve the employee list.");
        }
    }

    private setupTableButtons() {
        const editButtons = document.querySelectorAll('.btn-edit');
        editButtons.forEach(btn => {
            btn.addEventListener('click', (e) => {
                const id = (e.currentTarget as HTMLElement).getAttribute('data-id');
                if (id) this.openEditModal(id);
            });
        });

        const deleteButtons = document.querySelectorAll('.btn-delete');
        deleteButtons.forEach(btn => {
            btn.addEventListener('click', (e) => {
                const id = (e.currentTarget as HTMLElement).getAttribute('data-id');
                if (id) this.deleteEmployee(id);
            });
        });
    }

    private async askConfirmation(name: string): Promise<boolean> {
        const modal = document.getElementById('delete-confirm-modal');
        const nameSpan = document.getElementById('delete-employee-name');
        const confirmBtn = document.getElementById('confirm-delete');
        const cancelBtn = document.getElementById('cancel-delete');

        if (!modal || !nameSpan || !confirmBtn || !cancelBtn) return false;

        nameSpan.innerText = name;
        modal.style.display = 'flex'; 

        return new Promise((resolve) => {
            confirmBtn.onclick = () => {
                modal.style.display = 'none';
                resolve(true);
            };
            cancelBtn.onclick = () => {
                modal.style.display = 'none';
                resolve(false);
            };
        });
    }

    async deleteEmployee(id: string) {
        const employeeToDelete = this.employees.find(e => e.PersonID === id);
        if (!employeeToDelete) return;

        const confirmed = await this.askConfirmation(`${employeeToDelete.FirstName} ${employeeToDelete.LastName}`);

        if (confirmed) {
            try {
                const success = await EmployeeService.delete(employeeToDelete);
                if (success) {
                    this.fetchAndRender(document.getElementById('employee-list-body'));
                }
            }
            catch (error) {
                console.error("[EmployeeViewModel][deleteEmployee] failed:", error);
                this.showErrorModal("The server is not responding. The employee was not deleted.");
            }
        }
    }

    private render(container: HTMLElement) {
        container.innerHTML = this.employees.map(emp => `
        <tr>
            <td>${emp.EmployeeNo || 'N/A'}</td>
            <td>${emp.FirstName}</td>
            <td>${emp.LastName}</td>
            <td>
                <span class="status-badge ${emp.Status === 0 ? 'active' : 'inactive'}">
                    ${emp.Status === 0 ? 'Active' : 'Inactive'}
                </span>
            </td>
            <td>
                <button class="action-btn btn-edit" data-id="${emp.PersonID}"></button>
                <button class="action-btn btn-delete" data-id="${emp.PersonID}"></button>            
            </td>
        </tr>
    `).join('');

        this.setupTableButtons();
    }
}