export class ConfigService {
	private static settings: any = null;

	static async loadConfig() {
		if (this.settings) return;
		const response = await fetch('./appsettings.json');
		this.settings = await response.json();
	}

	static get api() {
		return this.settings.api;
	}
}