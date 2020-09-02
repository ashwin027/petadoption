import { SpaConfigService } from './spa-config.service';
import { SpaConfig } from '../models/spaConfig';

export function SpaConfigFactory(spaConfigService: SpaConfigService): () => Promise<SpaConfig> {
  return (): Promise<SpaConfig> => {
    return spaConfigService.getSpaConfig();
  };
}