import { Employee } from '../models/Employee.js';
import { ConfigService } from './ConfigService.js';

const API_URL = () => ConfigService.api.baseUrl;
const CUSTOMER_ID = () => ConfigService.api.customerId;
const API_KEY = () => ConfigService.api.apiKey;

export class EmployeeService {

    private static getHeaders(): HeadersInit {
        return {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'CustomerID': CUSTOMER_ID(),
            'APIKey': API_KEY()
        };
    }

    private static async handleResponse(response: Response) {
        if (!response.ok) {
            const errorData = await response.json().catch(() => ({}));
            throw errorData;
        }

        if (response.status === 204 || response.status === 205) {
            return true;
        }

        return await response.json().catch(() => ({ success: true }));
    }

    static async getAll(): Promise<Employee[]> {

        const response = await fetch(API_URL(), {
            method: 'GET',
            headers: this.getHeaders()
        });

        const data = await this.handleResponse(response);
        return data.value ? data.value : (Array.isArray(data) ? data : []);
    }

    static async create(employee: Employee): Promise<any> {
        const response = await fetch(API_URL(), {
            method: 'POST',
            headers: this.getHeaders(),
            body: JSON.stringify(employee)
        });

        return await this.handleResponse(response);
    }

    static async update(employee: Partial<Employee>): Promise<any> {
        const response = await fetch(API_URL(), {
            method: 'PUT',
            headers: this.getHeaders(),
            body: JSON.stringify(employee)
        });

        return await this.handleResponse(response);
    }

    static async delete(employee: any): Promise<any> {
        const url = `${API_URL()}('${employee.PersonID}')`;
        const response = await fetch(url, {
            method: 'DELETE',
            headers: this.getHeaders(),
            body: JSON.stringify(employee)
        });

        return await this.handleResponse(response);
    }
}
