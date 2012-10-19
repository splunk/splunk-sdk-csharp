/*
 * Copyright 2012 Splunk, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"): you may
 * not use this file except in compliance with the License. You may obtain
 * a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

namespace Splunk
{
    /// <summary>
    /// The EntityMetadata class provides access to the metadata properties
    /// of a corresponding entity. Use Entity.getMetadata to obtain an 
    /// instance of this class.
    /// </summary>
    public class EntityMetadata
    {
        /// <summary>
        /// The entity
        /// </summary>
        private Entity entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMetadata"/> 
        /// class.
        /// </summary>
        /// <param name="entity">The entity</param>
        public EntityMetadata(Entity entity)
        {
            this.entity = entity;
        }

        /// <summary>
        /// Gets a value indicating whether this entity's permission can be 
        /// changed.
        /// </summary>
        public bool CanChangePermissions
        {
            get
            {
                return this.GetEaiAcl().GetBoolean("can_change_perms", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this resource can be shared via an 
        /// app.
        /// </summary>
        public bool CanShareApp
        {
            get
            {
                return this.GetEaiAcl().GetBoolean("can_share_app", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the resource can be shared globally.
        /// </summary>
        public bool CanShareGlobal
        {
            get
            {
                return this.GetEaiAcl().GetBoolean("can_share_global", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the resource can be shared to a 
        /// specific user.
        /// </summary>
        public bool CanShareUser
        {
            get
            {
                return this.GetEaiAcl().GetBoolean("can_share_user", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether his entity can be modified.
        /// </summary>
        public bool CanWrite
        {
            get
            {
                return this.GetEaiAcl().GetBoolean("can_write", false);
            }
        }

        /// <summary>
        /// Gets the app context of this resource.
        /// </summary>
        public string App
        {
            get
            {
                return this.GetEaiAcl().GetString("app", "system");
            }
        }

        /// <summary>
        /// Gets the username of the resource owner.
        /// </summary>
        public string Owner 
        {
            get
            {
                return this.GetEaiAcl().GetString("owner");
            }
        }

        /// <summary>
        /// Gets this entity's permissions, which represent an
        /// allowable inclusive action:list-of-roles map.
        /// </summary>
        public Record Permissions
        {
            get
            {
                return (Record)this.GetEaiAcl().GetValue("perms", null);
            }
        }

        /// <summary>
        /// Gets how this resource is shared (app, global, and/or user).
        /// </summary>
        public string Sharing
        {
            get
            {
                return this.GetEaiAcl().GetString("sharing");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this entity can be modified.
        /// </summary>
        public bool IsModifiable
        {
            get
            {
                return this.GetEaiAcl().GetBoolean("modifiable", false);
            }
        }

        /// <summary>
        /// Returns a record containing all of the metadata information 
        /// for this resource.
        /// </summary>
        /// <returns>The metadata record</returns>
        public Record GetEaiAcl()
        {
            return (Record)this.entity.Validate().Get("eai:acl");
        }
    }
}
