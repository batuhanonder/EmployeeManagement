package com.batuhanonderr.spring.security.security.jwt;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;
import com.batuhanonderr.spring.security.security.services.UserDetailsImpl;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.stereotype.Component;

import io.jsonwebtoken.*;

@Component
public class JwtUtils {
	private static final Logger logger = LoggerFactory.getLogger(JwtUtils.class);

	@Value("${app.jwtSecret}")
	private String jwtSecret;

	@Value("${app.jwtExpirationMs}")
	private int jwtExpirationMs;

	public String generateJwtToken(Authentication authentication) {

		UserDetailsImpl userPrincipal = (UserDetailsImpl) authentication.getPrincipal();
		Collection<? extends GrantedAuthority> a = userPrincipal.getAuthorities();
		ArrayList<String> roles = new ArrayList<>();
		for(GrantedAuthority role : a){
			roles.add(role.getAuthority().toString());
		}
		Algorithm algorithm = Algorithm.HMAC256(jwtSecret);
		String token = JWT.create()
				.withClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userPrincipal.getUsername())
				.withArrayClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", roles.toArray(new String[0]))
				.withExpiresAt(new Date(System.currentTimeMillis() + jwtExpirationMs))
				.sign(algorithm);
		return token;
	}

	public String getUserNameFromJwtToken(String token) {
		return Jwts.parser().setSigningKey(jwtSecret).parseClaimsJws(token).getBody().getSubject();
	}

	public boolean validateJwtToken(String authToken) {
		try {
			Jwts.parser().setSigningKey(jwtSecret).parseClaimsJws(authToken);
			return true;
		} catch (SignatureException e) {
			logger.error("Invalid JWT signature: {}", e.getMessage());
		} catch (MalformedJwtException e) {
			logger.error("Invalid JWT token: {}", e.getMessage());
		} catch (ExpiredJwtException e) {
			logger.error("JWT token is expired: {}", e.getMessage());
		} catch (UnsupportedJwtException e) {
			logger.error("JWT token is unsupported: {}", e.getMessage());
		} catch (IllegalArgumentException e) {
			logger.error("JWT claims string is empty: {}", e.getMessage());
		}

		return false;
	}
}
