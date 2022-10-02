package com.batuhanonderr.spring.security.repository;

import java.util.Optional;

import com.batuhanonderr.spring.security.models.ERole;
import com.batuhanonderr.spring.security.models.Role;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface RoleRepository extends MongoRepository<Role, String> {
  Optional<Role> findByName(ERole name);
}
