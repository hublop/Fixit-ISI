import { JsonWebTokenUser } from './JsonWebTokenUser';
import { RefreshToken } from './RefreshToken';

export interface JsonWebToken {
    accessToken: string;
    user: JsonWebTokenUser;
    expiresIn: Date;
    refreshToken: RefreshToken;
}
